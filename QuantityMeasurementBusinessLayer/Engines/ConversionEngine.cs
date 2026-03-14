using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementBusinessLayer.Engines
{
    /// <summary>
    /// Static utility class handling unit conversions for all measurement types.
    /// Provides methods to convert between units and to/from base units.
    /// </summary>
    public static class ConversionEngine
    {
        /// <summary>
        /// Converts a quantity DTO value to its base unit equivalent.
        /// Base units: Inch (Length), Gram (Weight), Millilitre (Volume), Celsius (Temperature).
        /// </summary>
        /// <param name="dto">The quantity DTO containing value, unit, and measurement type</param>
        /// <returns>The value converted to the base unit for that measurement type</returns>
        /// <exception cref="QuantityMeasurementException">Thrown for invalid measurement types</exception>
        public static double ConvertToBase(QuantityDTO dto)
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
        /// Converts a value from base unit to the specified target unit.
        /// </summary>
        /// <param name="measurementType">The type of measurement (Length, Weight, Volume, Temperature)</param>
        /// <param name="unit">The target unit to convert to</param>
        /// <param name="baseValue">The value in base unit</param>
        /// <returns>The value converted to the specified unit</returns>
        /// <exception cref="QuantityMeasurementException">Thrown for invalid measurement types</exception>
        public static double ConvertFromBase(string measurementType, string unit, double baseValue)
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
    }
}