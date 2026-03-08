using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Generic Quantity class supporting any unit category.
    /// Supports conversion, addition, subtraction and division.
    /// U must be Enum (LengthUnit, WeightUnit, etc.)
    /// </summary>
    public sealed class Quantity<U> where U : Enum
    {
        public double Value { get; }
        public U Unit { get; }

        /// <summary>
        /// Constructor validates value and unit.
        /// Ensures immutability.
        /// </summary>
        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Value = value;
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        // ==========================================================
        // UC5–UC11: CONVERSION
        // ==========================================================

        /// <summary>
        /// Converts quantity to target unit.
        /// </summary>
        public Quantity<U> ConvertTo(U targetUnit)
        {
            double baseValue = ConvertToBase(Value, Unit);
            double converted = ConvertFromBase(baseValue, targetUnit);

            return new Quantity<U>(Math.Round(converted, 4), targetUnit);
        }

        // ==========================================================
        // UC6–UC11: ADDITION
        // ==========================================================

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

        // ==========================================================
        // UC12: SUBTRACTION
        // ==========================================================

        /// <summary>
        /// Subtracts another quantity and returns result in first unit.
        /// </summary>
        public Quantity<U> Subtract(Quantity<U> other)
            => Subtract(other, this.Unit);

        /// <summary>
        /// Subtracts another quantity and returns result in target unit.
        /// </summary>
        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double base1 = ConvertToBase(this.Value, this.Unit);
            double base2 = ConvertToBase(other.Value, other.Unit);

            double diffBase = base1 - base2;
            double result = ConvertFromBase(diffBase, targetUnit);

            return new Quantity<U>(Math.Round(result, 4), targetUnit);
        }

        // ==========================================================
        // UC12: DIVISION
        // ==========================================================

        /// <summary>
        /// Divides two quantities and returns a dimensionless ratio.
        /// </summary>
        public double Divide(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double base1 = ConvertToBase(this.Value, this.Unit);
            double base2 = ConvertToBase(other.Value, other.Unit);

            if (base2 == 0)
                throw new ArithmeticException("Division by zero is not allowed.");

            return base1 / base2;
        }

        // ==========================================================
        // UC1–UC11: EQUALITY COMPARISON
        // ==========================================================

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

        // ==========================================================
        // HELPER METHODS
        // ==========================================================

        /// <summary>
        /// Converts value to base unit depending on enum type.
        /// </summary>
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

        /// <summary>
        /// Converts base value to target unit.
        /// </summary>
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