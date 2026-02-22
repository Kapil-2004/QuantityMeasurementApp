using System;

namespace QuantityMeasurementApp.Models
{
    // Generic class representing a length measurement
    // Combines value and unit to eliminate duplication (DRY principle)
    public class QuantityLength
    {
        // Numeric measurement value
        public double Value { get; }

        // Unit of the measurement (Feet or Inch)
        public LengthUnit Unit { get; }

        // Constructor initializes value and unit
        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        // Converts current quantity to base unit (Feet)
        // Ensures consistent comparison between different units
        private double ConvertToFeet()
        {
            return Value * Unit.ToFeetFactor();
        }

        // Overrides Equals to implement value-based equality
        // Supports cross-unit comparison (e.g., 1 ft == 12 in)
        public override bool Equals(object obj)
        {
            // Return false if object is null or not same type
            if (obj == null || GetType() != obj.GetType())
                return false;

            QuantityLength other = (QuantityLength)obj;

            // Compare converted values in base unit (Feet)
            return ConvertToFeet().Equals(other.ConvertToFeet());
        }

        // Overrides GetHashCode to maintain equality contract consistency
        public override int GetHashCode()
        {
            return ConvertToFeet().GetHashCode();
        }
    }
}