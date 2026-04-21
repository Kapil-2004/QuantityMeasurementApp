namespace QuantityMeasurement.SharedModels.Models.Request
{
    public class BinaryOperationRequest
    {
        public QuantityRequest Q1 { get; set; }
        public QuantityRequest Q2 { get; set; }
    }

    public class QuantityRequest
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string MeasurementType { get; set; }
    }
}
