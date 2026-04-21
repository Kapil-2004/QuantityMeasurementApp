namespace QuantityMeasurement.SharedModels.Models.Response
{
    public class OperationHistoryResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OperationType { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
