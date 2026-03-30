namespace QuantityMeasurementModelLayer.Models.Response
{
    /// <summary>
    /// Response DTO for comparison operations
    /// </summary>
    public class ComparisonResponse
    {
        public bool AreEqual { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// Response DTO for arithmetic operations (Add, Subtract)
    /// </summary>
    public class ArithmeticOperationResponse
    {
        public double Result { get; set; }
        public string Unit { get; set; }
        public string MeasurementType { get; set; }
    }

    /// <summary>
    /// Response DTO for conversion operations
    /// </summary>
    public class ConversionResponse
    {
        public double Result { get; set; }
        public string Unit { get; set; }
        public string MeasurementType { get; set; }
    }

    /// <summary>
    /// Response DTO for division operations
    /// </summary>
    public class DivisionResponse
    {
        public double Result { get; set; }
    }

    /// <summary>
    /// Response DTO for operation history
    /// </summary>
    public class OperationHistoryResponse
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public object Operand1 { get; set; }
        public object Operand2 { get; set; }
        public object Result { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Response DTO for count
    /// </summary>
    public class CountResponse
    {
        public int TotalOperations { get; set; }
    }

    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ApiResponse(bool success, string message, T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    /// <summary>
    /// Error response DTO
    /// </summary>
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
