using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;

namespace QuantityMeasurementModelLayer.Entities
{
    public class QuantityMeasurementEntity
    {
        public QuantityModel<object>? Operand1 { get; set; }
        public QuantityModel<object>? Operand2 { get; set; }

        public OperationType Operation { get; set; }

        public object? Result { get; set; }

        public bool HasError { get; set; }

        public string? ErrorMessage { get; set; }

        public QuantityMeasurementEntity() { }

        public QuantityMeasurementEntity(object result)
        {
            Result = result;
            HasError = false;
        }

        public QuantityMeasurementEntity(string error)
        {
            HasError = true;
            ErrorMessage = error;
        }
    }
}