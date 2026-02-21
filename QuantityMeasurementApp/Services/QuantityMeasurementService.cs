using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        // Compare two Feet objects
        public bool AreEqual(Feet first, Feet second)
        {
            // Handle null cases safely
            if (first == null || second == null)
                return false;

            return first.Equals(second);
        }

        // Convert string input safely to Feet object
        public Feet CreateFeet(string input)
        {
            // TryParse prevents exception for non-numeric input
            if (!double.TryParse(input, out double value))
                return null;

            return new Feet(value);
        }

        // Compare two Inches objects
        public bool AreEqual(Inches first, Inches second)
        {
            // Null safety
            if (first == null || second == null)
                return false;

            return first.Equals(second);
        }

        // Create Inches object safely from string
        public Inches CreateInches(string input)
        {
            if (!double.TryParse(input, out double value))
                return null;

            return new Inches(value);
        }
    }
}