using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Length-specific service with convenient raw-value APIs.
    /// </summary>
    public class QuantityLengthService : QuantityService<LengthUnit>
    {
        public bool AreEqual(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            var q1 = new Quantity<LengthUnit>(value1, unit1);
            var q2 = new Quantity<LengthUnit>(value2, unit2);

            return base.AreEqual(q1, q2);
        }

        public double Convert(
            double value,
            LengthUnit from,
            LengthUnit to)
        {
            return base.ConvertTo(value, from, to);
        }

        public Quantity<LengthUnit> Add(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            var q1 = new Quantity<LengthUnit>(value1, unit1);
            var q2 = new Quantity<LengthUnit>(value2, unit2);

            return base.Add(q1, q2);
        }

        public Quantity<LengthUnit> Add(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2,
            LengthUnit targetUnit)
        {
            var q1 = new Quantity<LengthUnit>(value1, unit1);
            var q2 = new Quantity<LengthUnit>(value2, unit2);

            return base.Add(q1, q2, targetUnit);
        }
    }
}