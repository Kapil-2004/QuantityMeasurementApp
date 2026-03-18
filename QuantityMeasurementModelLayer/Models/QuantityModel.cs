namespace QuantityMeasurementModelLayer.Models
{
    public class QuantityModel<TUnit>
    {
        public double Value { get; set; }
        public TUnit Unit { get; set; }

        public QuantityModel(double value, TUnit unit)
        {
            Value = value;
            Unit = unit;
        }
    }
}