using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Temperature specific service.
    /// 
    /// Unlike Length, Weight, and Volume, temperature does NOT support
    /// arithmetic operations like addition, subtraction, or division.
    /// 
    /// Therefore this service exposes only:
    ///  - Equality comparison
    ///  - Unit conversion
    /// </summary>
    public class QuantityTemperatureService : QuantityService<TemperatureUnit>
    {
        /// <summary>
        /// Compares two temperature quantities for equality
        /// across different units.
        /// </summary>
        public bool AreEqual(
            double value1, TemperatureUnit unit1,
            double value2, TemperatureUnit unit2)
        {
            var q1 = new Quantity<TemperatureUnit>(value1, unit1);
            var q2 = new Quantity<TemperatureUnit>(value2, unit2);

            return base.AreEqual(q1, q2);
        }

        /// <summary>
        /// Converts temperature from one unit to another.
        /// </summary>
        public double Convert(
            double value,
            TemperatureUnit from,
            TemperatureUnit to)
        {
            return base.ConvertTo(value, from, to);
        }
    }
}