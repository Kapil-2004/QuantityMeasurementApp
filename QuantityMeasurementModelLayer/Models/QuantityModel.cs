namespace QuantityMeasurementModelLayer.Models
{
    /// <summary>
    /// Generic model class for representing a quantity with a value and associated unit.
    /// Used internally by the Service layer for type-safe operations.
    /// </summary>
    /// <typeparam name="TUnit">The type of unit (e.g., LengthUnit, WeightUnit, VolumeUnit, TemperatureUnit)</typeparam>
    public class QuantityModel<TUnit>
    {
        /// <summary>The numerical value of the quantity</summary>
        public double Value { get; set; }
        
        /// <summary>The unit associated with this quantity (type-safe)</summary>
        public TUnit Unit { get; set; }

        /// <summary>Constructor to initialize a QuantityModel with value and unit</summary>
        public QuantityModel(double value, TUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}