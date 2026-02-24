using System;

namespace QuantityMeasurementApp.Models
{
    // Represents a length value with unit
    public class QuantityLength
    {
        private const double TOLERANCE = 1e-6;

        public double Value { get; }
        public LengthUnit Unit { get; }

        // Constructor
        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be a valid number.");

            Value = value;
            Unit = unit;
        }

        // Convert current value to base unit (Feet)
        private double ConvertToBase()
        {
            return Value * Unit.ToFeetFactor();
        }

        // Static conversion API (UC5)
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (source == target)
                return value;

            double baseValue = value * source.ToFeetFactor();
            return baseValue / target.ToFeetFactor();
        }

        // Instance conversion
        public QuantityLength ConvertTo(LengthUnit target)
        {
            double converted = Convert(Value, Unit, target);
            return new QuantityLength(converted, target);
        }

        // Value-based equality
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            QuantityLength other = (QuantityLength)obj;

            return Math.Abs(ConvertToBase() - other.ConvertToBase()) < TOLERANCE;
        }

        public override int GetHashCode()
        {
            return ConvertToBase().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}