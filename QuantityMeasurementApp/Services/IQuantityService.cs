using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Generic service contract for Quantity operations.
    /// </summary>
    public interface IQuantityService<U> where U : Enum
    {
        bool AreEqual(Quantity<U> q1, Quantity<U> q2);

        Quantity<U> Add(Quantity<U> q1, Quantity<U> q2);

        Quantity<U> Add(Quantity<U> q1, Quantity<U> q2, U targetUnit);

        double ConvertTo(double value, U from, U to);
    }
}