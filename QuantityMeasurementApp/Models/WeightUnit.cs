namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Weight units implementing IMeasurable.
    /// Base Unit: Kilogram
    /// </summary>
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitExtensions
    {
        // Conversion factors relative to base (Kilogram)
        private static readonly Dictionary<WeightUnit, double> Factors =
            new()
            {
                { WeightUnit.Kilogram, 1.0 },
                { WeightUnit.Gram, 0.001 },
                { WeightUnit.Pound, 0.45359237 }
            };

        public static double GetConversionFactor(this WeightUnit unit)
            => Factors[unit];

        public static double ConvertToBase(this WeightUnit unit, double value)
            => value * unit.GetConversionFactor();

        public static double ConvertFromBase(this WeightUnit unit, double baseValue)
            => baseValue / unit.GetConversionFactor();

        public static string GetUnitName(this WeightUnit unit)
            => unit.ToString();
    }
}