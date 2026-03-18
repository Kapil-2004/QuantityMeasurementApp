using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using QuantityMeasurementModelLayer.Configuration;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Implementations
{
    public class QuantityMeasurementDatabaseRepository : IQuantityMeasurementRepository, IDisposable
    {
        private readonly string _connectionString;
        private readonly ILogger<QuantityMeasurementDatabaseRepository> _logger;
        private SqlConnection _connection;

        public QuantityMeasurementDatabaseRepository(ApplicationConfig config, ILogger<QuantityMeasurementDatabaseRepository> logger)
        {
            _connectionString = config.GetConnectionString("QuantityMeasurementDB");
            _logger = logger;
            _connection = new SqlConnection(_connectionString);
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            string measurementId = Guid.NewGuid().ToString();

            try
            {
                OpenConnection();

                // Insert into QuantityMeasurements table
                const string mainSql = @"
                    INSERT INTO QuantityMeasurements (
                        MeasurementId, CreatedAt, OperationType,
                        ResultValue, ResultUnit,
                        IsSuccessful, ErrorDetails
                    ) VALUES (
                        @MeasurementId, @CreatedAt, @OperationType,
                        @ResultValue, @ResultUnit,
                        @IsSuccessful, @ErrorDetails
                    )";

                using (var command = new SqlCommand(mainSql, _connection))
                {
                    command.Parameters.AddWithValue("@MeasurementId", measurementId);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@OperationType", (int)entity.Operation);

                    // Extract result value and unit based on result type
                    ExtractResultParameters(command, entity.Result);

                    command.Parameters.AddWithValue("@IsSuccessful", !entity.HasError);
                    command.Parameters.AddWithValue("@ErrorDetails", (object?)entity.ErrorMessage ?? DBNull.Value);

                    command.ExecuteNonQuery();
                    _logger.LogInformation("Inserted main measurement record {MeasurementId}", measurementId);
                }

                // Insert operands into MeasurementOperands table
                if (entity.Operand1 != null)
                {
                    InsertOperand(measurementId, 1, entity.Operand1);
                }

                if (entity.Operand2 != null)
                {
                    InsertOperand(measurementId, 2, entity.Operand2);
                }

                // Insert conversion target if applicable
                if (entity.Operand2 != null && entity.Operation == OperationType.Convert)
                {
                    InsertConversionTarget(measurementId, entity.Operand2.Unit.ToString());
                }

                _logger.LogInformation("Successfully saved measurement {MeasurementId} to database with result type {ResultType}.", 
                    measurementId, entity.Result?.GetType().Name ?? "null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save measurement to database.");
                throw new Exception("Failed to save measurement. See inner exception.", ex);
            }
        }

        private void ExtractResultParameters(SqlCommand command, object? result)
        {
            // Handle QuantityDTO result (Convert, Add, Subtract operations)
            if (result is QuantityDTO resultDto)
            {
                command.Parameters.AddWithValue("@ResultValue", resultDto.Value);
                command.Parameters.AddWithValue("@ResultUnit", resultDto.Unit);
                _logger.LogInformation("Extracted QuantityDTO result: Value={Value}, Unit={Unit}", resultDto.Value, resultDto.Unit);
            }
            // Handle QuantityModel result
            else if (result is QuantityModel<object> resultQuantity)
            {
                command.Parameters.AddWithValue("@ResultValue", resultQuantity.Value);
                command.Parameters.AddWithValue("@ResultUnit", resultQuantity.Unit.ToString());
                _logger.LogInformation("Extracted QuantityModel result: Value={Value}, Unit={Unit}", resultQuantity.Value, resultQuantity.Unit);
            }
            // Handle bool result (Compare operation)
            else if (result is bool resultBool)
            {
                command.Parameters.AddWithValue("@ResultValue", resultBool ? 1 : 0);
                command.Parameters.AddWithValue("@ResultUnit", "Boolean");
                _logger.LogInformation("Extracted bool result: Value={Value}", resultBool);
            }
            // Handle double result (Divide operation)
            else if (result is double resultDouble)
            {
                command.Parameters.AddWithValue("@ResultValue", resultDouble);
                command.Parameters.AddWithValue("@ResultUnit", "Numeric");
                _logger.LogInformation("Extracted double result: Value={Value}", resultDouble);
            }
            // Handle null or unsupported types
            else
            {
                command.Parameters.AddWithValue("@ResultValue", DBNull.Value);
                command.Parameters.AddWithValue("@ResultUnit", DBNull.Value);
                _logger.LogWarning("Result type {ResultType} not recognized, storing as NULL", result?.GetType().Name ?? "null");
            }
        }

        private void InsertOperand(string measurementId, int operandOrder, QuantityModel<object> operand)
        {
            const string sql = @"
                INSERT INTO MeasurementOperands (
                    MeasurementId, OperandOrder, OperandValue,
                    OperandUnit, OperandCategory
                ) VALUES (
                    @MeasurementId, @OperandOrder, @OperandValue,
                    @OperandUnit, @OperandCategory
                )";

            try
            {
                using var command = new SqlCommand(sql, _connection);
                command.Parameters.AddWithValue("@MeasurementId", measurementId);
                command.Parameters.AddWithValue("@OperandOrder", operandOrder);
                command.Parameters.AddWithValue("@OperandValue", operand.Value);
                command.Parameters.AddWithValue("@OperandUnit", operand.Unit.ToString());

                string category = operand.Unit.GetType().Name.Replace("Unit", "");
                command.Parameters.AddWithValue("@OperandCategory", category);

                command.ExecuteNonQuery();
                _logger.LogInformation("Inserted operand {OperandOrder} for measurement {MeasurementId}: Value={Value}, Unit={Unit}, Category={Category}",
                    operandOrder, measurementId, operand.Value, operand.Unit, category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert operand {OperandOrder} for measurement {MeasurementId}", operandOrder, measurementId);
                throw;
            }
        }

        private void InsertConversionTarget(string measurementId, string targetUnit)
        {
            const string sql = @"
                INSERT INTO ConversionTargets (
                    MeasurementId, TargetUnit
                ) VALUES (
                    @MeasurementId, @TargetUnit
                )";

            try
            {
                using var command = new SqlCommand(sql, _connection);
                command.Parameters.AddWithValue("@MeasurementId", measurementId);
                command.Parameters.AddWithValue("@TargetUnit", targetUnit);

                command.ExecuteNonQuery();
                _logger.LogInformation("Inserted conversion target for measurement {MeasurementId}: TargetUnit={TargetUnit}",
                    measurementId, targetUnit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert conversion target for measurement {MeasurementId}", measurementId);
                throw;
            }
        }


        public List<QuantityMeasurementEntity> GetAll()
        {
            const string sql = @"
                SELECT 
                    qm.MeasurementId, qm.CreatedAt, qm.OperationType, qm.ResultValue, qm.ResultUnit,
                    qm.IsSuccessful, qm.ErrorDetails,
                    mo.OperandId, mo.OperandOrder, mo.OperandValue, mo.OperandUnit, mo.OperandCategory,
                    ct.TargetUnit
                FROM QuantityMeasurements qm
                LEFT JOIN MeasurementOperands mo ON qm.MeasurementId = mo.MeasurementId
                LEFT JOIN ConversionTargets ct ON qm.MeasurementId = ct.MeasurementId
                ORDER BY qm.CreatedAt DESC";

            try
            {
                OpenConnection();
                using var command = new SqlCommand(sql, _connection);
                return GetMeasurementsByCommand(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all measurements.");
                throw new Exception("Failed to retrieve all measurements. See inner exception for details", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetMeasurementsByOperation(OperationType operationType)
        {
            const string sql = @"
                SELECT 
                    qm.MeasurementId, qm.CreatedAt, qm.OperationType, qm.ResultValue, qm.ResultUnit,
                    qm.IsSuccessful, qm.ErrorDetails,
                    mo.OperandId, mo.OperandOrder, mo.OperandValue, mo.OperandUnit, mo.OperandCategory,
                    ct.TargetUnit
                FROM QuantityMeasurements qm
                LEFT JOIN MeasurementOperands mo ON qm.MeasurementId = mo.MeasurementId
                LEFT JOIN ConversionTargets ct ON qm.MeasurementId = ct.MeasurementId
                WHERE qm.OperationType = @OperationType
                ORDER BY qm.CreatedAt DESC";

            try
            {
                OpenConnection();
                using var command = new SqlCommand(sql, _connection);
                command.Parameters.AddWithValue("@OperationType", (int)operationType);
                return GetMeasurementsByCommand(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve measurements by operation.");
                throw new Exception("Failed to retrieve measurements by operation. See inner exception for details", ex);
            }
        }

        private List<QuantityMeasurementEntity> GetMeasurementsByCommand(SqlCommand command)
        {
            var results = new List<QuantityMeasurementEntity>();
            try
            {
                command.Connection = _connection;
                using var reader = command.ExecuteReader();
                var measurementDict = new Dictionary<string, QuantityMeasurementEntity>();

                while (reader.Read())
                {
                    string measurementId = reader.GetString(reader.GetOrdinal("MeasurementId"));

                    // Create entity if it doesn't exist
                    if (!measurementDict.ContainsKey(measurementId))
                    {
                        measurementDict[measurementId] = MapReaderToEntity(reader);
                        _logger.LogInformation("Created entity for measurement {MeasurementId}", measurementId);
                    }

                    // Add operand if available
                    if (!reader.IsDBNull(reader.GetOrdinal("OperandOrder")))
                    {
                        int operandOrder = reader.GetInt32(reader.GetOrdinal("OperandOrder"));
                        double operandValue = reader.GetDouble(reader.GetOrdinal("OperandValue"));
                        string operandUnit = reader.GetString(reader.GetOrdinal("OperandUnit"));
                        string operandCategory = reader.GetString(reader.GetOrdinal("OperandCategory"));

                        // Reconstruct the enum from category and unit string
                        object unitEnum = ReconstructUnitEnum(operandCategory, operandUnit);
                        var operand = new QuantityModel<object>(operandValue, unitEnum);

                        if (operandOrder == 1)
                        {
                            measurementDict[measurementId].Operand1 = operand;
                            _logger.LogInformation("Set Operand1 for {MeasurementId}: Value={Value}, Unit={Unit}", 
                                measurementId, operandValue, operandUnit);
                        }
                        else if (operandOrder == 2)
                        {
                            measurementDict[measurementId].Operand2 = operand;
                            _logger.LogInformation("Set Operand2 for {MeasurementId}: Value={Value}, Unit={Unit}", 
                                measurementId, operandValue, operandUnit);
                        }
                    }
                }

                results = measurementDict.Values.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve measurements.");
                throw new Exception("Failed to retrieve measurements. See inner exception for details", ex);
            }

            return results;
        }

        private object ReconstructUnitEnum(string category, string unitString)
        {
            return category switch
            {
                "Length" => Enum.Parse<LengthUnit>(unitString),
                "Weight" => Enum.Parse<WeightUnit>(unitString),
                "Volume" => Enum.Parse<VolumeUnit>(unitString),
                "Temperature" => Enum.Parse<TemperatureUnit>(unitString),
                _ => throw new Exception($"Unknown category: {category}")
            };
        }

        public int GetTotalCount()
        {
            const string sql = "SELECT COUNT(*) FROM QuantityMeasurements";
            try
            {
                OpenConnection();
                using var command = new SqlCommand(sql, _connection);
                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get total count.");
                throw new Exception("Failed to get total count. See inner exception for details", ex);
            }
        }

        public void DeleteAll()
        {
            const string sql = "DELETE FROM QuantityMeasurements";
            try
            {
                OpenConnection();
                using var command = new SqlCommand(sql, _connection);
                command.ExecuteNonQuery();
                _logger.LogInformation("Deleted all measurements.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete all measurements.");
                throw new Exception("Failed to delete all measurements. See inner exception for details", ex);
            }
        }

        public void CloseResources()
        {
            Dispose();
        }

        private void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        private QuantityMeasurementEntity MapReaderToEntity(SqlDataReader reader)
        {
            var entity = new QuantityMeasurementEntity();
            
            entity.Operation = (OperationType)reader.GetInt32(reader.GetOrdinal("OperationType"));
            entity.HasError = !reader.GetBoolean(reader.GetOrdinal("IsSuccessful"));
            
            if (!reader.IsDBNull(reader.GetOrdinal("ErrorDetails")))
            {
                entity.ErrorMessage = reader.GetString(reader.GetOrdinal("ErrorDetails"));
            }

            // Map result value and unit
            if (!reader.IsDBNull(reader.GetOrdinal("ResultValue")) && !reader.IsDBNull(reader.GetOrdinal("ResultUnit")))
            {
                string resultUnit = reader.GetString(reader.GetOrdinal("ResultUnit"));
                object resultValue = reader.GetValue(reader.GetOrdinal("ResultValue"));

                // Create a QuantityModel with the result
                // Note: Enum conversion logic would go here based on resultUnit string
                entity.Result = $"{resultValue} {resultUnit}";
            }
            else if (entity.HasError)
            {
                entity.Result = null;
            }
            else
            {
                entity.Result = null;
            }

            return entity;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _logger.LogInformation("Database connection disposed.");
            }
        }
    }
}
