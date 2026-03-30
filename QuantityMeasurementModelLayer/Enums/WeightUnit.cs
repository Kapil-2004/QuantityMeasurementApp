namespace QuantityMeasurementModelLayer.Enums
{
    public enum WeightUnit
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitExtensions
    {
        public static string GetMeasurementType(this WeightUnit unit) => "Weight";

        public static double ToBaseUnit(this WeightUnit unit, double value)
        {
            return unit switch
            {
                WeightUnit.Kilogram => value * 1000,
                WeightUnit.Gram => value,
                WeightUnit.Pound => value * 453.592,
                _ => throw new ArgumentException("Invalid Weight Unit")
            };
        }

        public static double FromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return unit switch
            {
                WeightUnit.Kilogram => baseValue / 1000,
                WeightUnit.Gram => baseValue,
                WeightUnit.Pound => baseValue / 453.592,
                _ => throw new ArgumentException("Invalid Weight Unit")
            };
        }
    }
}
