using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Engines;

namespace QuantityMeasurement.ArithmeticService.Services
{
    public interface IArithmeticService
    {
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);
        double Divide(QuantityDTO q1, QuantityDTO q2);
    }

    public class ArithmeticServiceImpl : IArithmeticService
    {
        private readonly ILogger<ArithmeticServiceImpl> _logger;

        public ArithmeticServiceImpl(ILogger<ArithmeticServiceImpl> logger)
        {
            _logger = logger;
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                // Validate same measurement type
                ValidationEngine.ValidateSameMeasurement(q1.MeasurementType, q2.MeasurementType);

                // Convert both to base unit
                double q1BaseValue = ConversionEngine.ConvertToBase(q1);
                double q2BaseValue = ConversionEngine.ConvertToBase(q2);

                // Perform addition
                double resultBase = ArithmeticEngine.Add(q1BaseValue, q2BaseValue, q1.MeasurementType);

                // Convert back to q1 unit
                double resultInQ1Unit = ConversionEngine.ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

                _logger.LogInformation($"Add operation: {q1.Value}{q1.Unit} + {q2.Value}{q2.Unit} = {resultInQ1Unit}{q1.Unit}");

                return new QuantityDTO(resultInQ1Unit, q1.Unit, q1.MeasurementType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Add operation");
                throw;
            }
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                // Validate same measurement type
                ValidationEngine.ValidateSameMeasurement(q1.MeasurementType, q2.MeasurementType);

                // Convert both to base unit
                double q1BaseValue = ConversionEngine.ConvertToBase(q1);
                double q2BaseValue = ConversionEngine.ConvertToBase(q2);

                // Perform subtraction
                double resultBase = ArithmeticEngine.Subtract(q1BaseValue, q2BaseValue, q1.MeasurementType);

                // Convert back to q1 unit
                double resultInQ1Unit = ConversionEngine.ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

                _logger.LogInformation($"Subtract operation: {q1.Value}{q1.Unit} - {q2.Value}{q2.Unit} = {resultInQ1Unit}{q1.Unit}");

                return new QuantityDTO(resultInQ1Unit, q1.Unit, q1.MeasurementType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Subtract operation");
                throw;
            }
        }

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                // Validate same measurement type
                ValidationEngine.ValidateSameMeasurement(q1.MeasurementType, q2.MeasurementType);

                // Convert both to base unit
                double q1BaseValue = ConversionEngine.ConvertToBase(q1);
                double q2BaseValue = ConversionEngine.ConvertToBase(q2);

                // Perform division
                double result = ArithmeticEngine.Divide(q1BaseValue, q2BaseValue, q1.MeasurementType);

                _logger.LogInformation($"Divide operation: {q1.Value}{q1.Unit} / {q2.Value}{q2.Unit} = {result}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Divide operation");
                throw;
            }
        }
    }
}
