namespace QuantityMeasurementModelLayer.Enums
{
    /// <summary>
    /// Enumeration for temperature measurement units.
    /// Base unit for temperature conversions is Celsius.
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>Celsius unit (base unit for temperature)</summary>
        Celsius,
        
        /// <summary>Fahrenheit unit</summary>
        Fahrenheit,
        
        /// <summary>Kelvin unit</summary>
        Kelvin
    }

    /// <summary>
    /// Extension methods for TemperatureUnit enum providing measurement type and conversion methods.
    /// All conversions use Celsius as the base unit.
    /// </summary>
    public static class TemperatureUnitExtensions
    {
        /// <summary>Returns the measurement type name "Temperature"</summary>
        public static string GetMeasurementType(this TemperatureUnit unit) => "Temperature";

        /// <summary>
        /// Converts a value from the given temperature unit to the base unit (Celsius).
        /// </summary>
        public static double ToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.Celsius => value,                   // Already in base unit
                TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9, // Convert Fahrenheit to Celsius
                TemperatureUnit.Kelvin => value - 273.15,           // Convert Kelvin to Celsius
                _ => throw new ArgumentException("Invalid Temperature Unit")
            };
        }

        /// <summary>
        /// Converts a value from the base unit (Celsius) to the given temperature unit.
        /// </summary>
        public static double FromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.Celsius => baseValue,                   // Already in base unit
                TemperatureUnit.Fahrenheit => (baseValue * 9 / 5) + 32, // Convert Celsius to Fahrenheit
                TemperatureUnit.Kelvin => baseValue + 273.15,           // Convert Celsius to Kelvin
                _ => throw new ArgumentException("Invalid Temperature Unit")
            };
        }
    }
}