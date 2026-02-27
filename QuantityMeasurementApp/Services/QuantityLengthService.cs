using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    // Handles comparison and conversion logic
    public class QuantityLengthService
    {
        // Compare two quantities
        public bool AreEqual(double value1, LengthUnit unit1,
                             double value2, LengthUnit unit2)
        {
            var length1 = new QuantityLength(value1, unit1);
            var length2 = new QuantityLength(value2, unit2);

            return length1.Equals(length2);
        }

        // ============================================================
        // UC5 - Unit to Unit Conversion
        // ============================================================

        // Converts a value from source unit to target unit
        public double Convert(double value, LengthUnit source, LengthUnit target)
        {
            // Validate numeric input
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            // Convert source value to base unit (Feet)
            double valueInFeet = value * source.ToFeetFactor();

            // Convert from base unit to target unit
            double convertedValue = valueInFeet / target.ToFeetFactor();

            return convertedValue;
        }

        
        // ============================================================
        // UC6 - Addition Service Method
        // ============================================================

        public QuantityLength Add(double value1, LengthUnit unit1,
                                double value2, LengthUnit unit2)
        {
            var length1 = new QuantityLength(value1, unit1);
            var length2 = new QuantityLength(value2, unit2);

            return length1.Add(length2);
        }

        // ============================================================
        // UC7 - Addition with Target Unit Specification
        // ============================================================

        public QuantityLength Add(double value1, LengthUnit unit1,
                                double value2, LengthUnit unit2,
                                LengthUnit targetUnit)
        {
            var length1 = new QuantityLength(value1, unit1);
            var length2 = new QuantityLength(value2, unit2);

            return length1.Add(length2, targetUnit);
        }
    }
}