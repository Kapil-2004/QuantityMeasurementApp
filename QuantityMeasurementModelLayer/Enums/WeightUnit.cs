namespace QuantityMeasurementModelLayer.Enums
{
    /// <summary>
    /// Enumeration for weight measurement units.
    /// Base unit for weight conversions is Gram.
    /// </summary>
    public enum WeightUnit
    {
        /// <summary>Kilogram unit (1 kg = 1000 grams)</summary>
        Kilogram,
        
        /// <summary>Gram unit (base unit for weight)</summary>
        Gram,
        
        /// <summary>Pound unit</summary>
        Pound
    }

    /// <summary>
    /// Extension methods for WeightUnit enum providing measurement type and conversion methods.
    /// All conversions use Gram as the base unit.
    /// </summary>
    public static class WeightUnitExtensions
    {
        /// <summary>Returns the measurement type name "Weight"</summary>
        public static string GetMeasurementType(this WeightUnit unit) => "Weight";

        /// <summary>
        /// Converts a value from the given weight unit to the base unit (Gram).
        /// </summary>
        public static double ToBaseUnit(this WeightUnit unit, double value)
        {
            return unit switch
            {
                WeightUnit.Kilogram => value * 1000,     // 1 kg = 1000 grams
                WeightUnit.Gram => value,                 // Already in base unit
                WeightUnit.Pound => value * 453.592,      // 1 pound = 453.592 grams
                _ => throw new ArgumentException("Invalid Weight Unit")
            };
        }

        /// <summary>
        /// Converts a value from the base unit (Gram) to the given weight unit.
        /// </summary>
        public static double FromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return unit switch
            {
                WeightUnit.Kilogram => baseValue / 1000,     // Convert grams to kilograms
                WeightUnit.Gram => baseValue,                 // Already in base unit
                WeightUnit.Pound => baseValue / 453.592,      // Convert grams to pounds
                _ => throw new ArgumentException("Invalid Weight Unit")
            };
        }
    }
}