using System;

namespace QuantityMeasurementApp.Models
{
    // Enum defining supported length measurement units
    public enum LengthUnit
    {
        Feet,
        Inch,
        Yard,
        Centimeter
    }

    // Extension class to provide conversion logic for each unit
    public static class LengthUnitExtensions
    {
        // Converts the given unit into its equivalent factor in Feet (base unit)
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,                   // Base unit
                LengthUnit.Inch => 1.0 / 12.0,            // 12 Inches = 1 Foot
                LengthUnit.Yard => 3.0,                   // 1 Yard = 3 Feet
                LengthUnit.Centimeter => 0.0328084167,    // 1 cm = 0.0328084167 Feet
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}