using System;

namespace QuantityMeasurementApp.Models
{
    /*
     * UC8 REFACTORING
     * -------------------------------------------------------
     * LengthUnit is now a standalone enum.
     * It owns ALL conversion logic.
     * 
     * Responsibilities:
     * 1. Store conversion factor to base unit (Feet)
     * 2. Convert value to base unit
     * 3. Convert value from base unit
     * 
     * This ensures:
     * - Single Responsibility Principle
     * - Centralized unit logic
     * - Future scalability (WeightUnit, VolumeUnit, etc.)
     */

    public enum LengthUnit
    {
        Feet = 1,
        Inch = 2,
        Yard = 3,
        Centimeter = 4
    }

    public static class LengthUnitHelper
    {
        // ----------------------------------------------------
        // Conversion factors relative to BASE UNIT (Feet)
        // Base unit = Feet
        // ----------------------------------------------------
        private const double FEET_FACTOR = 1.0;
        private const double INCH_FACTOR = 1.0 / 12.0;
        private const double YARD_FACTOR = 3.0;
        private const double CM_FACTOR = 1.0 / 30.48;

        /*
         * Returns conversion factor for given unit.
         * Example:
         *   Inch -> 1/12
         *   Yard -> 3
         */
        public static double GetConversionFactor(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => FEET_FACTOR,
                LengthUnit.Inch => INCH_FACTOR,
                LengthUnit.Yard => YARD_FACTOR,
                LengthUnit.Centimeter => CM_FACTOR,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        /*
         * Converts a value from its unit to BASE UNIT (Feet)
         * Example:
         *   12 Inch -> 1 Feet
         */
        public static double ConvertToBaseUnit(LengthUnit unit, double value)
        {
            return value * GetConversionFactor(unit);
        }

        /*
         * Converts a BASE UNIT (Feet) value to target unit
         * Example:
         *   1 Feet -> 12 Inch
         */
        public static double ConvertFromBaseUnit(LengthUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }
    }
}