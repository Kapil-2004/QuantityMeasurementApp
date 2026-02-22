using System;

namespace QuantityMeasurementApp.Models
{
    // Enum defining supported length measurement units
    public enum LengthUnit
    {
        Feet,
        Inch
    }

    // Extension class to provide conversion logic for each unit
    public static class LengthUnitExtensions
    {
        // Converts the given unit into its equivalent factor in Feet (base unit)
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,          // 1 Foot = 1 Foot
                LengthUnit.Inch => 1.0 / 12.0,   // 12 Inches = 1 Foot
                _ => throw new ArgumentException("Unsupported unit")
            };
        }
    }
}