using System;

namespace QuantityMeasurementApp.Models
{
    public class Feet
    {
        // Private readonly field ensures immutability
        private readonly double _value;

        // Constructor to initialize feet value
        public Feet(double value)
        {
            _value = value;
        }

        // Read-only property to access value
        public double Value => _value;

        // Override Equals for value-based comparison
        public override bool Equals(object obj)
        {
            // Check same reference (reflexive property)
            if (this == obj)
                return true;

            // Null check and type check (type safety)
            if (obj == null || GetType() != obj.GetType())
                return false;

            // Safe casting
            Feet other = (Feet)obj;

            // Compare double values safely
            return this._value.Equals(other._value);
        }

        // When overriding Equals, always override GetHashCode
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}