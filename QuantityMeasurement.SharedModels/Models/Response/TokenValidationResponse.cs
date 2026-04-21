namespace QuantityMeasurement.SharedModels.Models.Response
{
    public class TokenValidationResponse
    {
        public bool IsValid { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
