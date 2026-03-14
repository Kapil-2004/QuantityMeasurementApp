using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementConsole.Controllers
{
    /// <summary>
    /// Console layer controller acting as a thin wrapper between UI (Menu) and business logic (Service).
    /// Follows N-Tier architecture pattern where Console Layer delegates all operations to Business Layer.
    /// Provides interface for Menu to execute quantity measurements without exposing service details.
    /// </summary>
    public class QuantityMeasurementController
    {
        /// <summary>Service instance for executing business logic operations</summary>
        private readonly IQuantityMeasurementService _service;

        /// <summary>
        /// Constructor accepting service dependency from DI container.
        /// Maintains separation of concerns by accepting abstraction (interface) not implementation.
        /// </summary>
        /// <param name="service">Business layer service implementing measurement operations</param>
        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            _service = service;
        }

        /// <summary>
        /// Delegates comparison operation to service layer.
        /// Thin wrapper method maintaining layer separation - no business logic here.
        /// </summary>
        /// <param name="q1">First quantity to compare</param>
        /// <param name="q2">Second quantity to compare</param>
        /// <returns>True if quantities are equal in base units, false otherwise</returns>
        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.Compare(q1, q2);
        }

        /// <summary>
        /// Delegates unit conversion operation to service layer.
        /// Converts quantity from its original unit to target unit maintaining accuracy.
        /// Uses base unit normalization for consistent conversions.
        /// </summary>
        /// <param name="source">Source quantity with original unit</param>
        /// <param name="targetUnit">Target unit to convert source quantity to</param>
        /// <returns>New QuantityDTO with converted value in target unit</returns>
        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            return _service.Convert(source, targetUnit);
        }

        /// <summary>
        /// Delegates addition operation to service layer.
        /// Adds two quantities of same measurement type.
        /// Service validates type compatibility, performs base unit conversion, arithmetic, and persistence.
        /// </summary>
        /// <param name="q1">First quantity operand</param>
        /// <param name="q2">Second quantity operand</param>
        /// <returns>QuantityDTO containing sum result in first quantity's unit</returns>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.Add(q1, q2);
        }

        /// <summary>
        /// Delegates subtraction operation to service layer.
        /// Subtracts second quantity from first quantity of same measurement type.
        /// Service validates type compatibility, performs base unit conversion, arithmetic, and persistence.
        /// </summary>
        /// <param name="q1">First quantity operand (minuend)</param>
        /// <param name="q2">Second quantity operand (subtrahend)</param>
        /// <returns>QuantityDTO containing difference result in first quantity's unit</returns>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.Subtract(q1, q2);
        }

        /// <summary>
        /// Delegates division operation to service layer.
        /// Divides first quantity by second quantity of same measurement type.
        /// Service validates type compatibility, checks for zero divisor, performs arithmetic.
        /// Result is unitless (ratio) since both quantities cancel out in division.
        /// </summary>
        /// <param name="q1">First quantity operand (dividend)</param>
        /// <param name="q2">Second quantity operand (divisor)</param>
        /// <returns>Unitless result as double (ratio of two quantities)</returns>
        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            return _service.Divide(q1, q2);
        }
    }
}