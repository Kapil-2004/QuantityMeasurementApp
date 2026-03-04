using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Weight-specific service with raw-value API.
    /// </summary>
    public class QuantityWeightService : QuantityService<WeightUnit>
    {
        public bool AreEqual(
            double value1, WeightUnit unit1,
            double value2, WeightUnit unit2)
        {
            var q1 = new Quantity<WeightUnit>(value1, unit1);
            var q2 = new Quantity<WeightUnit>(value2, unit2);

            return base.AreEqual(q1, q2);
        }

        public double Convert(
            double value,
            WeightUnit from,
            WeightUnit to)
        {
            return base.ConvertTo(value, from, to);
        }

        public Quantity<WeightUnit> Add(
            double value1, WeightUnit unit1,
            double value2, WeightUnit unit2)
        {
            var q1 = new Quantity<WeightUnit>(value1, unit1);
            var q2 = new Quantity<WeightUnit>(value2, unit2);

            return base.Add(q1, q2);
        }

        public Quantity<WeightUnit> Add(
            double value1, WeightUnit unit1,
            double value2, WeightUnit unit2,
            WeightUnit targetUnit)
        {
            var q1 = new Quantity<WeightUnit>(value1, unit1);
            var q2 = new Quantity<WeightUnit>(value2, unit2);

            return base.Add(q1, q2, targetUnit);
        }
    }
}