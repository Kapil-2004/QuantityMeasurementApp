namespace QuantityMeasurementBusinessLayer.Exceptions
{
    /// <summary>
    /// Custom exception class for domain-specific errors in the Quantity Measurement application.
    /// Used for validation errors, invalid operations, and business logic violations.
    /// </summary>
    public class QuantityMeasurementException : Exception
    {
        /// <summary>Constructor accepting an error message</summary>
        public QuantityMeasurementException(string message) : base(message)
        {
        }

        /// <summary>Constructor accepting an error message and inner exception for exception wrapping</summary>
        public QuantityMeasurementException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}