using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Generic Quantity class supporting any unit category.
    /// U must be Enum (LengthUnit, WeightUnit, etc.)
    /// </summary>
    public sealed class Quantity<U> where U : Enum
    {
        public double Value { get; }
        public U Unit { get; }

        /// <summary>
        /// Constructor validates value and unit.
        /// </summary>
        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Value = value;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        /// <summary>
        /// Converts quantity to target unit.
        /// </summary>
        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase(Value, Unit);
            double converted = ConvertFromBase(baseValue, targetUnit);

            return new Quantity<U>(Math.Round(converted, 4), targetUnit);
        }

        /// <summary>
        /// Adds two quantities and returns result in first unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> other)
            => Add(other, this.Unit);

        /// <summary>
        /// Adds two quantities and returns result in target unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double base1 = ConvertToBase(this.Value, this.Unit);
            double base2 = ConvertToBase(other.Value, other.Unit);

            double sumBase = base1 + base2;
            double result = ConvertFromBase(sumBase, targetUnit);

            return new Quantity<U>(Math.Round(result, 4), targetUnit);
        }

        /// <summary>
        /// Equality comparison using base unit normalization.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            double base1 = ConvertToBase(this.Value, this.Unit);
            double base2 = ConvertToBase(other.Value, other.Unit);

            return Math.Abs(base1 - base2) < 0.0001;
        }

        public override int GetHashCode()
        {
            double baseValue = ConvertToBase(Value, Unit);
            return baseValue.GetHashCode();
        }

        public override string ToString()
            => $"Quantity({Value}, {Unit})";

        // Helper: Convert to base depending on enum type
        private double ConvertToBase(double value, U unit)
        {
            if (unit is LengthUnit length)
                return length.ConvertToBase(value);

            if (unit is WeightUnit weight)
                return weight.ConvertToBase(value);

            if (unit is VolumeUnit volume)
                return volume.ConvertToBase(value);

            throw new InvalidOperationException("Unsupported unit type.");
        }

        private double ConvertFromBase(double baseValue, U unit)
        {
            if (unit is LengthUnit length)
                return length.ConvertFromBase(baseValue);

            if (unit is WeightUnit weight)
                return weight.ConvertFromBase(baseValue);

            if (unit is VolumeUnit volume)
                return volume.ConvertFromBase(baseValue);

            throw new InvalidOperationException("Unsupported unit type.");
        }
    }
}