using System;

namespace QuantityMeasurementApp.Models
{
    // Generic class representing a length measurement
    // Combines value and unit (DRY compliant design)
    public class QuantityLength
    {
        private const double TOLERANCE = 0.0001;

        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        // Converts quantity into base unit (Feet)
        private double ConvertToFeet()
        {
            return Value * Unit.ToFeetFactor();
        }

        // Implements value-based equality with tolerance
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            QuantityLength other = (QuantityLength)obj;

            return Math.Abs(ConvertToFeet() - other.ConvertToFeet()) < TOLERANCE;
        }

        // Maintains equality contract
        public override int GetHashCode()
        {
            return ConvertToFeet().GetHashCode();
        }
    }
}