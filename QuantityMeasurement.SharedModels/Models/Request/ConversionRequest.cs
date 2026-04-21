namespace QuantityMeasurement.SharedModels.Models.Request
{
    public class ConversionRequest
    {
        public QuantityRequest Quantity { get; set; }
        public string TargetUnit { get; set; }
    }
}
