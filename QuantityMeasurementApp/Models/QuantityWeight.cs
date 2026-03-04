using System;

namespace QuantityMeasurementApp.Models
{
    /*
     * UC9 - QUANTITY WEIGHT CLASS
     * -------------------------------------------------------
     * Responsible for:
     * - Holding value + WeightUnit
     * - Equality comparison
     * - Unit conversion
     * - Addition operations
     *
     * It delegates conversion responsibility
     * to WeightUnitHelper.
     *
     * Weight and Length are COMPLETELY SEPARATE categories.
     */

    public class QuantityWeight
    {
        private const double TOLERANCE = 1e-4;

        public double Value { get; }
        public WeightUnit Unit { get; }

        public QuantityWeight(double value, WeightUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (!Enum.IsDefined(typeof(WeightUnit), unit))
                throw new ArgumentException("Invalid weight unit.");

            Value = value;
            Unit = unit;
        }

        // ----------------------------------------------------
        // Convert current object to BASE UNIT (Kilogram)
        // ----------------------------------------------------
        private double ConvertToBase()
        {
            return WeightUnitHelper.ConvertToBaseUnit(Unit, Value);
        }

        // ----------------------------------------------------
        // UC9 - Convert to another unit
        // ----------------------------------------------------
        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Invalid target unit.");

            double baseValue = ConvertToBase();

            double convertedValue =
                WeightUnitHelper.ConvertFromBaseUnit(targetUnit, baseValue);

            return new QuantityWeight(convertedValue, targetUnit);
        }

        // ----------------------------------------------------
        // Add two weights (result in first unit)
        // ----------------------------------------------------
        public QuantityWeight Add(QuantityWeight other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            double finalValue =
                WeightUnitHelper.ConvertFromBaseUnit(this.Unit, baseSum);

            return new QuantityWeight(finalValue, this.Unit);
        }

        // ----------------------------------------------------
        // Add with explicit target unit
        // ----------------------------------------------------
        public QuantityWeight Add(QuantityWeight other, WeightUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!Enum.IsDefined(typeof(WeightUnit), targetUnit))
                throw new ArgumentException("Invalid target unit.");

            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            double finalValue =
                WeightUnitHelper.ConvertFromBaseUnit(targetUnit, baseSum);

            return new QuantityWeight(finalValue, targetUnit);
        }

        // ----------------------------------------------------
        // Equality (cross unit supported)
        // ----------------------------------------------------
        public override bool Equals(object obj)
        {
            if (obj is not QuantityWeight other)
                return false;

            return Math.Abs(this.ConvertToBase() - other.ConvertToBase()) < TOLERANCE;
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