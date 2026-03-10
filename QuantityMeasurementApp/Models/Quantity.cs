using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Generic Quantity class representing a measurable quantity.
    /// Works with LengthUnit, WeightUnit, VolumeUnit etc.
    /// </summary>
    public class Quantity<U> where U : Enum
    {
        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            Value = value;
            Unit = unit;
        }

        // ==========================================================
        // UC13 – ENUM FOR ARITHMETIC OPERATIONS
        // ==========================================================

        /// <summary>
        /// Enum used to dispatch arithmetic operations
        /// without using if-else or switch statements.
        /// </summary>
        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        // ==========================================================
        // UC13 – VALIDATION HELPER
        // ==========================================================

        /// <summary>
        /// Centralized validation for arithmetic operations.
        /// Ensures consistent validation across add/subtract/divide.
        /// </summary>
        private void ValidateArithmeticOperands(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentException("Operand quantity cannot be null.");

            if (!double.IsFinite(Value) || !double.IsFinite(other.Value))
                throw new ArgumentException("Quantity values must be finite numbers.");

            // Ensure both quantities belong to same measurement category
            if (Unit.GetType() != other.Unit.GetType())
                throw new ArgumentException("Cannot operate on quantities of different categories.");
        }

        // ==========================================================
        // UC14 – CORE ARITHMETIC HELPER
        // ==========================================================

        /// <summary>
        /// Performs arithmetic in BASE UNITS.
        /// All arithmetic operations delegate here.
        /// </summary>
        private double PerformBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            ValidateArithmeticOperands(other);

            IMeasurable thisUnit = new MeasurableUnitAdapter(Unit);

            // ==========================================================
            // UC14 – Validate operation support BEFORE arithmetic
            // ==========================================================
            thisUnit.ValidateOperationSupport(operation.ToString());

            IMeasurable otherUnit = new MeasurableUnitAdapter(other.Unit);

            // Convert both values to base unit
            double baseValue1 = thisUnit.ConvertToBase(Value);
            double baseValue2 = otherUnit.ConvertToBase(other.Value);

            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return baseValue1 + baseValue2;

                case ArithmeticOperation.SUBTRACT:
                    return baseValue1 - baseValue2;

                case ArithmeticOperation.DIVIDE:

                    if (baseValue2 == 0)
                        throw new ArithmeticException("Division by zero is not allowed.");

                    return baseValue1 / baseValue2;

                default:
                    throw new InvalidOperationException("Unsupported arithmetic operation.");
            }
        }

        // ==========================================================
        // UC13 – ADD OPERATIONS
        // ==========================================================

        /// <summary>
        /// Adds two quantities and returns result in first operand unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> other)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);

            IMeasurable thisUnit = new MeasurableUnitAdapter(Unit);

            double result = thisUnit.ConvertFromBase(baseResult);

            return new Quantity<U>(RoundToTwoDecimals(result), Unit);
        }

        /// <summary>
        /// Adds two quantities and returns result in target unit.
        /// </summary>
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);

            IMeasurable target = new MeasurableUnitAdapter(targetUnit);

            double result = target.ConvertFromBase(baseResult);

            return new Quantity<U>(RoundToTwoDecimals(result), targetUnit);
        }

        // ==========================================================
        // UC13 – SUBTRACT OPERATIONS
        // ==========================================================

        public Quantity<U> Subtract(Quantity<U> other)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

            IMeasurable thisUnit = new MeasurableUnitAdapter(Unit);

            double result = thisUnit.ConvertFromBase(baseResult);

            return new Quantity<U>(RoundToTwoDecimals(result), Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

            IMeasurable target = new MeasurableUnitAdapter(targetUnit);

            double result = target.ConvertFromBase(baseResult);

            return new Quantity<U>(RoundToTwoDecimals(result), targetUnit);
        }

        // ==========================================================
        // UC13 – DIVIDE OPERATION
        // ==========================================================

        /// <summary>
        /// Division returns a scalar value (dimensionless).
        /// </summary>
        public double Divide(Quantity<U> other)
        {
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        // ==========================================================
        // HELPER – ROUNDING
        // ==========================================================

        /// <summary>
        /// Ensures consistent rounding across add/subtract operations.
        /// </summary>
        private double RoundToTwoDecimals(double value)
        {
            return Math.Round(value, 4);
        }

        // ==========================================================
        // EQUALITY COMPARISON
        // ==========================================================

        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            // Convert both to base unit and compare
            IMeasurable thisAdapter = new MeasurableUnitAdapter(Unit);
            IMeasurable otherAdapter = new MeasurableUnitAdapter(other.Unit);

            double thisBaseValue = thisAdapter.ConvertToBase(Value);
            double otherBaseValue = otherAdapter.ConvertToBase(other.Value);

            return Math.Abs(thisBaseValue - otherBaseValue) < 0.0001;
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + Value.GetHashCode();
            result = prime * result + (Unit?.GetHashCode() ?? 0);
            return result;
        }

        // ==========================================================
        // ToString Override
        // ==========================================================

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            IMeasurable thisUnit = new MeasurableUnitAdapter(Unit);
            IMeasurable target = new MeasurableUnitAdapter(targetUnit);

            double baseValue = thisUnit.ConvertToBase(Value);
            double result = target.ConvertFromBase(baseValue);

            return new Quantity<U>(RoundToTwoDecimals(result), targetUnit);
        }
    }
}