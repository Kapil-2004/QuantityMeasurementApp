using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;

namespace QuantityMeasurementModelLayer.Entities
{
    /// <summary>
    /// Entity class representing a complete operation record stored in the Repository.
    /// Contains information about the operation performed, operands, result, and any errors.
    /// </summary>
    public class QuantityMeasurementEntity
    {
        /// <summary>First operand used in the operation (nullable)</summary>
        public QuantityModel<object>? Operand1 { get; set; }
        
        /// <summary>Second operand used in the operation (nullable)</summary>
        public QuantityModel<object>? Operand2 { get; set; }

        /// <summary>Type of operation performed (Compare, Convert, Add, Subtract, or Divide)</summary>
        public OperationType Operation { get; set; }

        /// <summary>Result of the operation (bool, QuantityDTO, or double depending on operation)</summary>
        public object? Result { get; set; }

        /// <summary>Flag indicating whether an error occurred during the operation</summary>
        public bool HasError { get; set; }

        /// <summary>Error message if an error occurred during the operation</summary>
        public string? ErrorMessage { get; set; }

        /// <summary>Default constructor</summary>
        public QuantityMeasurementEntity() { }

        /// <summary>Constructor for storing a successful operation result</summary>
        public QuantityMeasurementEntity(object result)
        {
            Result = result;
            HasError = false;
        }

        /// <summary>Constructor for storing an operation error</summary>
        public QuantityMeasurementEntity(string error)
        {
            HasError = true;
            ErrorMessage = error;
        }
    }
}