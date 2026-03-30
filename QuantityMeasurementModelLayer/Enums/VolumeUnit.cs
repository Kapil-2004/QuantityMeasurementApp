namespace QuantityMeasurementModelLayer.Enums
{
    public enum VolumeUnit
    {
        Litre,
        Millilitre,
        Gallon
    }

    public static class VolumeUnitExtensions
    {
        public static string GetMeasurementType(this VolumeUnit unit) => "Volume";

        public static double ToBaseUnit(this VolumeUnit unit, double value)
        {
            return unit switch
            {
                VolumeUnit.Litre => value * 1000,
                VolumeUnit.Millilitre => value,
                VolumeUnit.Gallon => value * 3785.41,
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }

        public static double FromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return unit switch
            {
                VolumeUnit.Litre => baseValue / 1000,
                VolumeUnit.Millilitre => baseValue,
                VolumeUnit.Gallon => baseValue / 3785.41,
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }
    }
}
