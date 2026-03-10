using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Adapter to make enum units work with IMeasurable interface.
    /// Wraps unit enums and delegates to their extension methods.
    /// </summary>
    internal class MeasurableUnitAdapter : IMeasurable
    {
        private readonly object _unit;
        private readonly Type _unitType;

        public MeasurableUnitAdapter(object unit)
        {
            _unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _unitType = unit.GetType();
        }

        public double ConvertToBase(double value)
        {
            // Call the appropriate extension method based on unit type
            if (_unit is LengthUnit lengthUnit)
                return lengthUnit.ConvertToBase(value);
            if (_unit is WeightUnit weightUnit)
                return weightUnit.ConvertToBase(value);
            if (_unit is VolumeUnit volumeUnit)
                return volumeUnit.ConvertToBase(value);
            if (_unit is TemperatureUnit tempUnit)
                return tempUnit.ConvertToBase(value);

            throw new NotSupportedException($"Unit type {_unitType.Name} is not supported.");
        }

        public double ConvertFromBase(double baseValue)
        {
            // Call the appropriate extension method based on unit type
            if (_unit is LengthUnit lengthUnit)
                return lengthUnit.ConvertFromBase(baseValue);
            if (_unit is WeightUnit weightUnit)
                return weightUnit.ConvertFromBase(baseValue);
            if (_unit is VolumeUnit volumeUnit)
                return volumeUnit.ConvertFromBase(baseValue);
            if (_unit is TemperatureUnit tempUnit)
                return tempUnit.ConvertFromBase(baseValue);

            throw new NotSupportedException($"Unit type {_unitType.Name} is not supported.");
        }

        public double GetConversionFactor()
        {
            // Call the appropriate extension method based on unit type
            if (_unit is LengthUnit lengthUnit)
                return lengthUnit.GetConversionFactor();
            if (_unit is WeightUnit weightUnit)
                return weightUnit.GetConversionFactor();
            if (_unit is VolumeUnit volumeUnit)
                return volumeUnit.GetConversionFactor();
            if (_unit is TemperatureUnit)
                return 1.0; // Temperature doesn't have a simple conversion factor

            throw new NotSupportedException($"Unit type {_unitType.Name} is not supported.");
        }

        public string GetUnitName()
        {
            if (_unit is LengthUnit lengthUnit)
                return lengthUnit.GetUnitName();
            if (_unit is WeightUnit weightUnit)
                return weightUnit.GetUnitName();
            if (_unit is VolumeUnit volumeUnit)
                return volumeUnit.GetUnitName();
            if (_unit is TemperatureUnit tempUnit)
                return tempUnit.GetUnitName();

            throw new NotSupportedException($"Unit type {_unitType.Name} is not supported.");
        }

        public bool SupportsArithmetic()
        {
            if (_unit is TemperatureUnit tempUnit)
                return tempUnit.SupportsArithmetic();

            return true; // Default: all other units support arithmetic
        }

        public void ValidateOperationSupport(string operation)
        {
            if (_unit is TemperatureUnit tempUnit)
                tempUnit.ValidateOperationSupport(operation);
            // Default: no validation needed for other units
        }
    }
}
