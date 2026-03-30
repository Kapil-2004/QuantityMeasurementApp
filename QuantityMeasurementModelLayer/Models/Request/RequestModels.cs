using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.Models.Request
{
    /// <summary>
    /// Request DTO for quantity-based operations
    /// </summary>
    public class QuantityRequest
    {
        [Required(ErrorMessage = "Value is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be a non-negative number")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "MeasurementType is required")]
        [StringLength(50, ErrorMessage = "MeasurementType cannot exceed 50 characters")]
        public string MeasurementType { get; set; }
    }

    /// <summary>
    /// Request DTO for two-quantity operations (Add, Subtract, Compare, Divide)
    /// </summary>
    public class BinaryOperationRequest
    {
        [Required(ErrorMessage = "First quantity is required")]
        public QuantityRequest Q1 { get; set; }

        [Required(ErrorMessage = "Second quantity is required")]
        public QuantityRequest Q2 { get; set; }
    }

    /// <summary>
    /// Request DTO for conversion operations
    /// </summary>
    public class ConversionRequest
    {
        [Required(ErrorMessage = "Quantity is required")]
        public QuantityRequest Quantity { get; set; }

        [Required(ErrorMessage = "Target unit is required")]
        [StringLength(50, ErrorMessage = "TargetUnit cannot exceed 50 characters")]
        public string TargetUnit { get; set; }
    }
}
