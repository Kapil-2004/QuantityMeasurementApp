using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Core generic implementation for quantity operations.
    /// Works with Quantity<U> objects.
    /// </summary>
    public class QuantityService<U> : IQuantityService<U>
        where U : Enum
    {
        public bool AreEqual(Quantity<U> q1, Quantity<U> q2)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException(nameof(q1), "Quantity cannot be null.");

            return q1.Equals(q2);
        }

        public Quantity<U> Add(Quantity<U> q1, Quantity<U> q2)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException(nameof(q1), "Quantity cannot be null.");

            return q1.Add(q2);
        }

        public Quantity<U> Add(Quantity<U> q1, Quantity<U> q2, U targetUnit)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException(nameof(q1), "Quantity cannot be null.");

            return q1.Add(q2, targetUnit);
        }

        public double ConvertTo(double value, U from, U to)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException($"Value must be a finite number. Received: {value}");

            var quantity = new Quantity<U>(value, from);
            var converted = quantity.ConvertTo(to);
            return converted.Value;
        }
    }
}