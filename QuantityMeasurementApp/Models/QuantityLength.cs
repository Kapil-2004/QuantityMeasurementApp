using System;

namespace QuantityMeasurementApp.Models
{
    /*
     * UC8 SIMPLIFIED DESIGN
     * -------------------------------------------------------
     * QuantityLength is now ONLY responsible for:
     * - Holding value + unit
     * - Equality comparison
     * - Arithmetic (Addition)
     * 
     * It NO LONGER knows conversion factors.
     * It delegates conversion to LengthUnitHelper.
     */

    public class QuantityLength
    {
        private const double TOLERANCE = 1e-6;

        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            // Validate numeric value
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            // Validate enum
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit.");

            Value = value;
            Unit = unit;
        }

        // ----------------------------------------------------
        // Converts current object to base unit (Feet)
        // Delegates to LengthUnitHelper
        // ----------------------------------------------------
        private double ConvertToBase()
        {
            return LengthUnitHelper.ConvertToBaseUnit(Unit, Value);
        }

        // ----------------------------------------------------
        // UC5 - Convert to another unit
        // ----------------------------------------------------
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ConvertToBase();
            double convertedValue =
                LengthUnitHelper.ConvertFromBaseUnit(targetUnit, baseValue);

            return new QuantityLength(convertedValue, targetUnit);
        }

        // ----------------------------------------------------
        // UC6 - Add two quantities (result in first unit)
        // ----------------------------------------------------
        public QuantityLength Add(QuantityLength other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Convert both to base unit
            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            // Convert result back to first unit
            double finalValue =
                LengthUnitHelper.ConvertFromBaseUnit(this.Unit, baseSum);

            return new QuantityLength(finalValue, this.Unit);
        }

        // ----------------------------------------------------
        // UC7 - Add with explicit target unit
        // ----------------------------------------------------
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit.");

            double baseSum = this.ConvertToBase() + other.ConvertToBase();

            double finalValue =
                LengthUnitHelper.ConvertFromBaseUnit(targetUnit, baseSum);

            return new QuantityLength(finalValue, targetUnit);
        }

        // ----------------------------------------------------
        // UC1–UC4 Equality (cross unit supported)
        // ----------------------------------------------------
        public override bool Equals(object obj)
        {
            if (obj is not QuantityLength other)
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