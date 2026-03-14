using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Engines
{
    /// <summary>
    /// Static utility class for validating measurement operations.
    /// Ensures that operations are only performed on compatible measurement types.
    /// </summary>
    public static class ValidationEngine
    {
        /// <summary>
        /// Validates that two quantities have the same measurement type.
        /// Throws QuantityMeasurementException if they don't match.
        /// </summary>
        /// <param name="q1">First quantity to validate</param>
        /// <param name="q2">Second quantity to validate</param>
        /// <exception cref="QuantityMeasurementException">Thrown when measurement types don't match</exception>
        public static void ValidateSameMeasurement(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1.MeasurementType != q2.MeasurementType)
                throw new QuantityMeasurementException("Different measurement types not allowed");
        }
    }
}