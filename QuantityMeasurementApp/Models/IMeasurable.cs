using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Defines common behavior for all measurable units.
    /// UC14 introduces optional arithmetic support validation.
    /// </summary>
    public interface IMeasurable
    {
        // ==========================================================
        // CORE CONVERSION METHODS
        // ==========================================================

        double GetConversionFactor();

        double ConvertToBase(double value);

        double ConvertFromBase(double baseValue);

        string GetUnitName();

        // ==========================================================
        // UC14 – OPTIONAL ARITHMETIC SUPPORT
        // ==========================================================

        /// <summary>
        /// Indicates whether arithmetic operations are supported.
        /// Default implementation allows arithmetic.
        /// Temperature will override this behavior.
        /// </summary>
        bool SupportsArithmetic()
        {
            return true;
        }

        /// <summary>
        /// Validates whether a specific operation is supported.
        /// Default implementation allows all operations.
        /// </summary>
        void ValidateOperationSupport(string operation)
        {
            // Default: no restriction
        }
    }
}