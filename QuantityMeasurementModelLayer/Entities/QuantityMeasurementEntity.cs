using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;

namespace QuantityMeasurementModelLayer.Entities
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }

        public QuantityModel<object>? Operand1 { get; set; }
        public QuantityModel<object>? Operand2 { get; set; }

        public OperationType Operation { get; set; }

        public object? Result { get; set; }

        public bool HasError { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

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
