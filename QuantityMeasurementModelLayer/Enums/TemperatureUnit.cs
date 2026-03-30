namespace QuantityMeasurementModelLayer.Enums
{
    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }

    public static class TemperatureUnitExtensions
    {
        public static string GetMeasurementType(this TemperatureUnit unit) => "Temperature";

        public static double ToBaseUnit(this TemperatureUnit unit, double value)
        {
            return unit switch
            {
                TemperatureUnit.Celsius => value,
                TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
                TemperatureUnit.Kelvin => value - 273.15,
                _ => throw new ArgumentException("Invalid Temperature Unit")
            };
        }

        public static double FromBaseUnit(this TemperatureUnit unit, double baseValue)
        {
            return unit switch
            {
                TemperatureUnit.Celsius => baseValue,
                TemperatureUnit.Fahrenheit => (baseValue * 9 / 5) + 32,
                TemperatureUnit.Kelvin => baseValue + 273.15,
                _ => throw new ArgumentException("Invalid Temperature Unit")
            };
        }
    }
}
