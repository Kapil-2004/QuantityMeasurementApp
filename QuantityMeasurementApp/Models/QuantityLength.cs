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

        // ============================================================
        // UC6 - Addition of Two Lengths
        // ============================================================

        // Adds another QuantityLength and returns result
        // in the unit of the current object (first operand)
        public QuantityLength Add(QuantityLength other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Validate values
            if (!double.IsFinite(this.Value) || !double.IsFinite(other.Value))
                throw new ArgumentException("Invalid numeric value");

            // Convert both to base unit (Feet)
            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            // Convert result back to first operand's unit
            double finalValue = baseSum / this.Unit.ToFeetFactor();

            return new QuantityLength(finalValue, this.Unit);
        }

        // ============================================================
        // UC7 - Addition with Explicit Target Unit
        // ============================================================

        // Overloaded Add method
        // Adds another QuantityLength and returns result
        // in the explicitly specified target unit
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Validate numeric values
            if (!double.IsFinite(this.Value) || !double.IsFinite(other.Value))
                throw new ArgumentException("Invalid numeric value");

            // Validate target unit (enum cannot be null but defensive check)
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            // Convert both to base unit (Feet)
            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            // Convert base sum to explicitly specified target unit
            double finalValue = baseSum / targetUnit.ToFeetFactor();

            return new QuantityLength(finalValue, targetUnit);
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