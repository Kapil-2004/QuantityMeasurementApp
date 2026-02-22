using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    // Service class to separate business logic from Program.cs
    public class QuantityLengthService
    {
        // Compares two quantities for equality
        // Reduces dependency on main method
        public bool AreEqual(double value1, LengthUnit unit1,
                             double value2, LengthUnit unit2)
        {
            // Create first quantity object
            QuantityLength q1 = new QuantityLength(value1, unit1);

            // Create second quantity object
            QuantityLength q2 = new QuantityLength(value2, unit2);

            // Return equality result
            return q1.Equals(q2);
        }
    }
}