using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Temperature units implementing IMeasurable.
    /// Base Unit: Celsius
    /// </summary>
    public enum TemperatureUnit
    {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    public static class TemperatureUnitExtensions
    {
        // ==========================================================
        // UC14 – TEMPERATURE DOES NOT SUPPORT ARITHMETIC
        // ==========================================================

        public static bool SupportsArithmetic(this TemperatureUnit unit)
        {
            return false;
        }

        public static void ValidateOperationSupport(this TemperatureUnit unit, string operation)
        {
            throw new UnsupportedOperationException(
                $"Temperature does not support '{operation}' operation because arithmetic on absolute temperatures is not meaningful.");
        }

        // ==========================================================
        // CONVERSION METHODS
        // ==========================================================

        /// <summary>
        /// Convert temperature to base unit (Celsius)
        /// </summary>
        public static double ConvertToBase(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => value,
                TemperatureUnit.FAHRENHEIT => (value - 32) * 5 / 9,
                TemperatureUnit.KELVIN => value - 273.15,
                _ => throw new ArgumentException("Invalid temperature unit")
            };
        }

        /// <summary>
        /// Convert base Celsius value to target unit
        /// </summary>
        public static double ConvertFromBase(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.CELSIUS => baseValue,
                TemperatureUnit.FAHRENHEIT => (baseValue * 9 / 5) + 32,
                TemperatureUnit.KELVIN => baseValue + 273.15,
                _ => throw new ArgumentException("Invalid temperature unit")
            };
        }

        public static double GetConversionFactor(this TemperatureUnit unit)
        {
            return 1.0;
        }

        public static string GetUnitName(this TemperatureUnit unit)
        {
            return unit.ToString();
        }
    }

    /// <summary>
    /// Custom exception for unsupported operations
    /// </summary>
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string message) : base(message) { }
    }
}