namespace QuantityMeasurementModelLayer.Enums
{
    /// <summary>
    /// Enumeration for length measurement units.
    /// Base unit for length conversions is Inch.
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>Feet unit (1 foot = 12 inches)</summary>
        Feet,
        
        /// <summary>Inch unit (base unit for length)</summary>
        Inch,
        
        /// <summary>Yard unit (1 yard = 36 inches)</summary>
        Yard,
        
        /// <summary>Centimeter unit</summary>
        Centimeter
    }

    /// <summary>
    /// Extension methods for LengthUnit enum providing measurement type and conversion methods.
    /// All conversions use Inch as the base unit.
    /// </summary>
    public static class LengthUnitExtensions
    {
        /// <summary>Returns the measurement type name "Length"</summary>
        public static string GetMeasurementType(this LengthUnit unit) => "Length";

        /// <summary>
        /// Converts a value from the given length unit to the base unit (Inch).
        /// </summary>
        /// <param name="unit">The length unit to convert from</param>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted value in inches (base unit)</returns>
        public static double ToBaseUnit(this LengthUnit unit, double value)
        {
            return unit switch
            {
                LengthUnit.Feet => value * 12,           // 1 foot = 12 inches
                LengthUnit.Inch => value,                 // Already in base unit
                LengthUnit.Yard => value * 36,            // 1 yard = 36 inches
                LengthUnit.Centimeter => value * 0.393701, // 1 cm = 0.393701 inches
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }

        /// <summary>
        /// Converts a value from the base unit (Inch) to the given length unit.
        /// </summary>
        /// <param name="unit">The length unit to convert to</param>
        /// <param name="baseValue">The value in inches (base unit)</param>
        /// <returns>The converted value in the specified unit</returns>
        public static double FromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return unit switch
            {
                LengthUnit.Feet => baseValue / 12,           // Convert inches to feet
                LengthUnit.Inch => baseValue,                 // Already in base unit
                LengthUnit.Yard => baseValue / 36,            // Convert inches to yards
                LengthUnit.Centimeter => baseValue / 0.393701, // Convert inches to cm
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }
    }
}