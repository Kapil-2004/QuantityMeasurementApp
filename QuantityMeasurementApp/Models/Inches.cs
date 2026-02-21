using System;

namespace QuantityMeasurementApp.Models
{
    public class Inches
    {
        // Immutable value field
        private readonly double _value;

        // Constructor
        public Inches(double value)
        {
            _value = value;
        }

        // Read-only property
        public double Value => _value;

        // Override Equals for value-based equality
        public override bool Equals(object obj)
        {
            // Same reference check
            if (this == obj)
                return true;

            // Null and type check
            if (obj == null || GetType() != obj.GetType())
                return false;

            Inches other = (Inches)obj;

            // Floating point comparison
            return Double.Equals(this._value, other._value);
        }

        // Override GetHashCode
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}