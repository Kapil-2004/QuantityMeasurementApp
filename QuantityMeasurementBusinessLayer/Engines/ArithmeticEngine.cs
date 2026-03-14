using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Engines
{
    /// <summary>
    /// Static utility class for performing arithmetic operations on measurements.
    /// Handles addition, subtraction, and division with type-specific validation.
    /// Note: Temperature does not support arithmetic operations.
    /// </summary>
    public static class ArithmeticEngine
    {
        /// <summary>
        /// Adds two values (expected to be in base units).
        /// Temperature addition is not supported.
        /// </summary>
        /// <param name="v1">First value to add</param>
        /// <param name="v2">Second value to add</param>
        /// <param name="measurementType">Type of measurement (Length, Weight, Volume, Temperature)</param>
        /// <returns>The sum of v1 and v2</returns>
        /// <exception cref="QuantityMeasurementException">Thrown if measurement type is Temperature</exception>
        public static double Add(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature addition not supported");

            return v1 + v2;
        }

        /// <summary>
        /// Subtracts one value from another (expected to be in base units).
        /// Temperature subtraction is not supported.
        /// </summary>
        /// <param name="v1">Value to subtract from</param>
        /// <param name="v2">Value to subtract</param>
        /// <param name="measurementType">Type of measurement</param>
        /// <returns>The difference (v1 - v2)</returns>
        /// <exception cref="QuantityMeasurementException">Thrown if measurement type is Temperature</exception>
        public static double Subtract(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature subtraction not supported");

            return v1 - v2;
        }

        /// <summary>
        /// Divides one value by another (expected to be in base units).
        /// Temperature division is not supported.
        /// </summary>
        /// <param name="v1">Dividend value</param>
        /// <param name="v2">Divisor value (cannot be zero)</param>
        /// <param name="measurementType">Type of measurement</param>
        /// <returns>The quotient (v1 / v2)</returns>
        /// <exception cref="QuantityMeasurementException">Thrown if measurement type is Temperature or if v2 is zero</exception>
        public static double Divide(double v1, double v2, string measurementType)
        {
            if (measurementType == "Temperature")
                throw new QuantityMeasurementException("Temperature division not supported");

            if (v2 == 0)
                throw new QuantityMeasurementException("Division by zero");

            return v1 / v2;
        }
    }
}