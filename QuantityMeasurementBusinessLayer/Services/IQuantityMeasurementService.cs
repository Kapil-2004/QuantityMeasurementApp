using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Services
{
    /// <summary>
    /// Service interface defining all business operations for quantity measurements.
    /// Implementations handle validation, conversion, and arithmetic operations.
    /// </summary>
    public interface IQuantityMeasurementService
    {
        /// <summary>
        /// Compares two quantities to check if they are equal (after converting to base units).
        /// </summary>
        /// <param name="q1">First quantity to compare</param>
        /// <param name="q2">Second quantity to compare</param>
        /// <returns>True if quantities are equal, false otherwise</returns>
        bool Compare(QuantityDTO q1, QuantityDTO q2);

        /// <summary>
        /// Converts a quantity from one unit to another unit of the same measurement type.
        /// </summary>
        /// <param name="input">The quantity to convert</param>
        /// <param name="targetUnit">The target unit to convert to</param>
        /// <returns>A new QuantityDTO with the converted value and unit</returns>
        QuantityDTO Convert(QuantityDTO input, string targetUnit);

        /// <summary>
        /// Adds two quantities of the same measurement type.
        /// Result is expressed in the unit of the first quantity.
        /// Temperature addition is not supported.
        /// </summary>
        /// <param name="q1">First quantity to add</param>
        /// <param name="q2">Second quantity to add</param>
        /// <returns>Result of addition as a QuantityDTO</returns>
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);

        /// <summary>
        /// Subtracts one quantity from another of the same measurement type.
        /// Result is expressed in the unit of the first quantity.
        /// Temperature subtraction is not supported.
        /// </summary>
        /// <param name="q1">Quantity to subtract from</param>
        /// <param name="q2">Quantity to subtract</param>
        /// <returns>Result of subtraction as a QuantityDTO</returns>
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);

        /// <summary>
        /// Divides one quantity by another of the same measurement type.
        /// Returns a dimensionless double result.
        /// Temperature division is not supported.
        /// </summary>
        /// <param name="q1">Dividend quantity</param>
        /// <param name="q2">Divisor quantity (cannot result in zero after conversion)</param>
        /// <returns>The result of the division as a double</returns>
        double Divide(QuantityDTO q1, QuantityDTO q2);
    }
}