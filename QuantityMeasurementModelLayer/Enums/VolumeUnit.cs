namespace QuantityMeasurementModelLayer.Enums
{
    /// <summary>
    /// Enumeration for volume measurement units.
    /// Base unit for volume conversions is Millilitre.
    /// </summary>
    public enum VolumeUnit
    {
        /// <summary>Litre unit (1 liter = 1000 millilitres)</summary>
        Litre,
        
        /// <summary>Millilitre unit (base unit for volume)</summary>
        Millilitre,
        
        /// <summary>Gallon unit</summary>
        Gallon
    }

    /// <summary>
    /// Extension methods for VolumeUnit enum providing measurement type and conversion methods.
    /// All conversions use Millilitre as the base unit.
    /// </summary>
    public static class VolumeUnitExtensions
    {
        /// <summary>Returns the measurement type name "Volume"</summary>
        public static string GetMeasurementType(this VolumeUnit unit) => "Volume";

        /// <summary>
        /// Converts a value from the given volume unit to the base unit (Millilitre).
        /// </summary>
        public static double ToBaseUnit(this VolumeUnit unit, double value)
        {
            return unit switch
            {
                VolumeUnit.Litre => value * 1000,        // 1 liter = 1000 millilitres
                VolumeUnit.Millilitre => value,           // Already in base unit
                VolumeUnit.Gallon => value * 3785.41,     // 1 gallon = 3785.41 millilitres
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }

        /// <summary>
        /// Converts a value from the base unit (Millilitre) to the given volume unit.
        /// </summary>
        public static double FromBaseUnit(this VolumeUnit unit, double baseValue)
        {
            return unit switch
            {
                VolumeUnit.Litre => baseValue / 1000,        // Convert millilitres to litres
                VolumeUnit.Millilitre => baseValue,           // Already in base unit
                VolumeUnit.Gallon => baseValue / 3785.41,     // Convert millilitres to gallons
                _ => throw new ArgumentException("Invalid Volume Unit")
            };
        }
    }
}