using System.Collections.Generic;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Volume units implementing IMeasurable.
    /// Base Unit: Litre
    /// </summary>
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon
    }

    /// <summary>
    /// Extension methods implementing IMeasurable behavior
    /// for VolumeUnit enum.
    /// </summary>
    public static class VolumeUnitExtensions
    {
        // Conversion factors relative to base unit (Litre)
        private static readonly Dictionary<VolumeUnit, double> Factors =
            new()
            {
                { VolumeUnit.Litre, 1.0 },
                { VolumeUnit.Millilitre, 0.001 },
                { VolumeUnit.Gallon, 3.78541 }
            };

        /// <summary>
        /// Returns conversion factor relative to base unit (Litre)
        /// </summary>
        public static double GetConversionFactor(this VolumeUnit unit)
            => Factors[unit];

        /// <summary>
        /// Converts given value to base unit (Litre)
        /// </summary>
        public static double ConvertToBase(this VolumeUnit unit, double value)
            => value * unit.GetConversionFactor();

        /// <summary>
        /// Converts base unit (Litre) value to target unit
        /// </summary>
        public static double ConvertFromBase(this VolumeUnit unit, double baseValue)
            => baseValue / unit.GetConversionFactor();

        /// <summary>
        /// Returns readable name of unit
        /// </summary>
        public static string GetUnitName(this VolumeUnit unit)
            => unit.ToString();
    }
}