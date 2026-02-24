using System;

namespace QuantityMeasurementApp.Models
{
    // Supported units
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    // Extension method for conversion factor to base unit (Feet)
    public static class LengthUnitExtensions
    {
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inch => 1.0 / 12.0,
                LengthUnit.Yard => 3.0,
                LengthUnit.Centimeter => 0.0328084167,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}