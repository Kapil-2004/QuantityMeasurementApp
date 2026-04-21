namespace QuantityMeasurement.SharedModels.Exceptions
{
    public class DatabaseException : QuantityMeasurementException
    {
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
