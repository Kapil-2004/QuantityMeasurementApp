using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /*
     * UC9 - WEIGHT SERVICE
     * -------------------------------------------------------
     * Provides service-level abstraction
     * for equality, conversion and addition.
     */

    public class QuantityWeightService
    {
        // Compare two weights
        public bool AreEqual(double value1, WeightUnit unit1,
                             double value2, WeightUnit unit2)
        {
            var w1 = new QuantityWeight(value1, unit1);
            var w2 = new QuantityWeight(value2, unit2);

            return w1.Equals(w2);
        }

        // Convert weight
        public double Convert(double value, WeightUnit source, WeightUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value");

            double baseValue =
                WeightUnitHelper.ConvertToBaseUnit(source, value);

            return WeightUnitHelper.ConvertFromBaseUnit(target, baseValue);
        }

        // Add (default first unit)
        public QuantityWeight Add(double value1, WeightUnit unit1,
                                  double value2, WeightUnit unit2)
        {
            var w1 = new QuantityWeight(value1, unit1);
            var w2 = new QuantityWeight(value2, unit2);

            return w1.Add(w2);
        }

        // Add with target unit
        public QuantityWeight Add(double value1, WeightUnit unit1,
                                  double value2, WeightUnit unit2,
                                  WeightUnit targetUnit)
        {
            var w1 = new QuantityWeight(value1, unit1);
            var w2 = new QuantityWeight(value2, unit2);

            return w1.Add(w2, targetUnit);
        }
    }
}