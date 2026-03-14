namespace QuantityMeasurementModelLayer.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for transferring quantity measurement data between layers.
    /// Contains the measured value, unit of measurement, and the type of measurement.
    /// </summary>
    public class QuantityDTO
    {
        /// <summary>The numerical value of the quantity (e.g., 10 for 10 feet)</summary>
        public double Value { get; set; }
        
        /// <summary>The unit of measurement (e.g., "Feet", "Kilogram", "Litre")</summary>
        public string Unit { get; set; }
        
        /// <summary>The type of measurement (e.g., "Length", "Weight", "Volume", "Temperature")</summary>
        public string MeasurementType { get; set; }

        /// <summary>Constructor to initialize a QuantityDTO with value, unit, and measurement type</summary>
        public QuantityDTO(double value, string unit, string measurementType)
        {
            Value = value;
            Unit = unit;
            MeasurementType = measurementType;
        }
    }
}