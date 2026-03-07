using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Volume specific service using raw-value API.
    /// Works exactly like Length and Weight services.
    /// </summary>
    public class QuantityVolumeService : QuantityService<VolumeUnit>
    {
        /// <summary>
        /// Compares two volume quantities for equality
        /// </summary>
        public bool AreEqual(
            double value1, VolumeUnit unit1,
            double value2, VolumeUnit unit2)
        {
            var q1 = new Quantity<VolumeUnit>(value1, unit1);
            var q2 = new Quantity<VolumeUnit>(value2, unit2);

            return base.AreEqual(q1, q2);
        }

        /// <summary>
        /// Converts volume from one unit to another
        /// </summary>
        public double Convert(
            double value,
            VolumeUnit from,
            VolumeUnit to)
        {
            return base.ConvertTo(value, from, to);
        }

        /// <summary>
        /// Adds two volume quantities
        /// Result unit = first operand unit
        /// </summary>
        public Quantity<VolumeUnit> Add(
            double value1, VolumeUnit unit1,
            double value2, VolumeUnit unit2)
        {
            var q1 = new Quantity<VolumeUnit>(value1, unit1);
            var q2 = new Quantity<VolumeUnit>(value2, unit2);

            return base.Add(q1, q2);
        }

        /// <summary>
        /// Adds two volumes and converts result to target unit
        /// </summary>
        public Quantity<VolumeUnit> Add(
            double value1, VolumeUnit unit1,
            double value2, VolumeUnit unit2,
            VolumeUnit targetUnit)
        {
            var q1 = new Quantity<VolumeUnit>(value1, unit1);
            var q2 = new Quantity<VolumeUnit>(value2, unit2);

            return base.Add(q1, q2, targetUnit);
        }
    }
}