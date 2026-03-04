namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Length units implementing IMeasurable.
    /// Base Unit: Feet
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    public static class LengthUnitExtensions
    {
        // Conversion factors relative to base (Feet)
        private static readonly Dictionary<LengthUnit, double> Factors =
            new()
            {
                { LengthUnit.Feet, 1.0 },
                { LengthUnit.Inch, 1.0 / 12 },
                { LengthUnit.Yard, 3.0 },
                { LengthUnit.Centimeter, 0.0328084167 }
            };

        public static double GetConversionFactor(this LengthUnit unit)
        {
            // Professional validation
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException($"Invalid LengthUnit: {unit}");

            return Factors[unit];
        }

        public static double ConvertToBase(this LengthUnit unit, double value)
            => value * unit.GetConversionFactor();

        public static double ConvertFromBase(this LengthUnit unit, double baseValue)
            => baseValue / unit.GetConversionFactor();

        public static string GetUnitName(this LengthUnit unit)
            => unit.ToString();
    }
}