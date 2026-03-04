namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Defines common behavior for all measurable units.
    /// Enforces conversion capability across categories.
    /// </summary>
    public interface IMeasurable
    {
        double GetConversionFactor();              // Factor relative to base unit
        double ConvertToBase(double value);        // Convert to base unit
        double ConvertFromBase(double baseValue);  // Convert from base unit
        string GetUnitName();                      // Readable name
    }
}