using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementBusinessLayer.Services
{
    /// <summary>
    /// Implementation of IQuantityMeasurementService providing core business logic for quantity operations.
    /// Handles comparison, conversion, and arithmetic operations with repository persistence.
    /// </summary>
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        /// <summary>Repository instance for storing operation results</summary>
        private readonly IQuantityMeasurementRepository repository;

        /// <summary>Constructor accepting a repository dependency for persistence</summary>
        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Private helper method to convert a quantity DTO to its base unit value.
        /// Used internally for all calculations.
        /// </summary>
        private double ConvertToBase(QuantityDTO dto)
        {
            return dto.MeasurementType switch
            {
                "Length" => Enum.Parse<LengthUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Weight" => Enum.Parse<WeightUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Volume" => Enum.Parse<VolumeUnit>(dto.Unit).ToBaseUnit(dto.Value),
                "Temperature" => Enum.Parse<TemperatureUnit>(dto.Unit).ToBaseUnit(dto.Value),
                _ => throw new QuantityMeasurementException("Invalid Measurement Type")
            };
        }

        /// <summary>
        /// Private helper method to convert a value from base unit to the specified target unit.
        /// Used to format results back to the user's preferred unit.
        /// </summary>
        private double ConvertFromBase(string measurementType, string unit, double baseValue)
        {
            return measurementType switch
            {
                "Length" => Enum.Parse<LengthUnit>(unit).FromBaseUnit(baseValue),
                "Weight" => Enum.Parse<WeightUnit>(unit).FromBaseUnit(baseValue),
                "Volume" => Enum.Parse<VolumeUnit>(unit).FromBaseUnit(baseValue),
                "Temperature" => Enum.Parse<TemperatureUnit>(unit).FromBaseUnit(baseValue),
                _ => throw new QuantityMeasurementException("Invalid Measurement Type")
            };
        }

        /// <summary>
        /// Compares two quantities after converting to their base units.
        /// Stores result in repository for audit trail.
        /// </summary>
        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot compare different measurement types");

            // Convert both quantities to base units for accurate comparison
            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            bool result = base1 == base2;

            // Store comparison result in repository
            repository.Save(new QuantityMeasurementEntity(result));

            return result;
        }

        /// <summary>
        /// Converts a quantity from one unit to another within the same measurement type.
        /// Stores result in repository for audit trail.
        /// </summary>
        public QuantityDTO Convert(QuantityDTO input, string targetUnit)
        {
            // Step 1: Convert input to base unit
            double baseValue = ConvertToBase(input);

            // Step 2: Convert from base unit to target unit
            double convertedValue = ConvertFromBase(input.MeasurementType, targetUnit, baseValue);

            // Step 3: Create result DTO with converted value
            var result = new QuantityDTO(convertedValue, targetUnit, input.MeasurementType);

            // Step 4: Store result in repository
            repository.Save(new QuantityMeasurementEntity(result));

            return result;
        }

        /// <summary>
        /// Adds two quantities of the same measurement type.
        /// Result is expressed in the unit of the first quantity.
        /// Stores result in repository for audit trail.
        /// </summary>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate same measurement type
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot add different measurement types");

            // Temperature addition not supported
            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature addition not supported");

            // Convert both quantities to base units
            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            // Perform addition in base units
            double resultBase = base1 + base2;

            // Convert result back to q1's original unit
            double resultValue = ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

            // Create result DTO
            var result = new QuantityDTO(resultValue, q1.Unit, q1.MeasurementType);

            // Store result in repository
            repository.Save(new QuantityMeasurementEntity(result));

            return result;
        }

        /// <summary>
        /// Subtracts one quantity from another of the same measurement type.
        /// Result is expressed in the unit of the first quantity.
        /// Stores result in repository for audit trail.
        /// </summary>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate same measurement type
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot subtract different measurement types");

            // Temperature subtraction not supported
            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature subtraction not supported");

            // Convert both quantities to base units
            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            // Perform subtraction in base units
            double resultBase = base1 - base2;

            // Convert result back to q1's original unit
            double resultValue = ConvertFromBase(q1.MeasurementType, q1.Unit, resultBase);

            // Create result DTO
            var result = new QuantityDTO(resultValue, q1.Unit, q1.MeasurementType);

            // Store result in repository
            repository.Save(new QuantityMeasurementEntity(result));

            return result;
        }

        /// <summary>
        /// Divides one quantity by another of the same measurement type.
        /// Returns a dimensionless double result (ratio).
        /// Stores result in repository for audit trail.
        /// </summary>
        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate same measurement type
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Cannot divide different measurement types");

            // Temperature division not supported
            if (q1.MeasurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature division not supported");

            // Convert both quantities to base units
            double base1 = ConvertToBase(q1);
            double base2 = ConvertToBase(q2);

            // Check for division by zero
            if (base2 == 0)
                throw new QuantityMeasurementException("Division by zero");

            // Perform division (dimensionless result)
            double result = base1 / base2;

            // Store result in repository
            repository.Save(new QuantityMeasurementEntity(result));

            return result;
        }
    }
}