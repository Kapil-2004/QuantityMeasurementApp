namespace QuantityMeasurement.SharedModels.Models.Response
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ErrorResponse(string message, List<string> errors = null)
        {
            Success = false;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public ErrorResponse() { }
    }
}
