namespace QuantityMeasurementModelLayer.Enums
{
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    public static class LengthUnitExtensions
    {
        public static string GetMeasurementType(this LengthUnit unit) => "Length";

        public static double ToBaseUnit(this LengthUnit unit, double value)
        {
            return unit switch
            {
                LengthUnit.Feet => value * 12,
                LengthUnit.Inch => value,
                LengthUnit.Yard => value * 36,
                LengthUnit.Centimeter => value * 0.393701,
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }

        public static double FromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return unit switch
            {
                LengthUnit.Feet => baseValue / 12,
                LengthUnit.Inch => baseValue,
                LengthUnit.Yard => baseValue / 36,
                LengthUnit.Centimeter => baseValue / 0.393701,
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }
    }
}
