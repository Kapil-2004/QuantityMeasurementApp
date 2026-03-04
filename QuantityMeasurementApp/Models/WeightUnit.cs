using System;

namespace QuantityMeasurementApp.Models
{
    /*
     * UC9 - WEIGHT UNIT ENUM
     * -------------------------------------------------------
     * WeightUnit is a standalone enum.
     * It owns ALL conversion logic (just like LengthUnitHelper).
     *
     * Base Unit = Kilogram
     *
     * Supported Units:
     * 1. Kilogram
     * 2. Gram
     * 3. Pound
     *
     * This ensures:
     * - Single Responsibility Principle
     * - Centralized weight conversion logic
     * - Clean separation from Length category
     */

    public enum WeightUnit
    {
        Kilogram = 1,
        Gram = 2,
        Pound = 3
    }

    public static class WeightUnitHelper
    {
        // ----------------------------------------------------
        // Conversion factors relative to BASE UNIT (Kilogram)
        // ----------------------------------------------------
        private const double KILOGRAM_FACTOR = 1.0;
        private const double GRAM_FACTOR = 0.001;      // 1 g = 0.001 kg
        private const double POUND_FACTOR = 0.453592;  // 1 lb = 0.453592 kg

        /*
         * Returns conversion factor for given unit.
         */
        public static double GetConversionFactor(WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.Kilogram => KILOGRAM_FACTOR,
                WeightUnit.Gram => GRAM_FACTOR,
                WeightUnit.Pound => POUND_FACTOR,
                _ => throw new ArgumentException("Unsupported weight unit")
            };
        }

        /*
         * Converts value to BASE UNIT (Kilogram)
         */
        public static double ConvertToBaseUnit(WeightUnit unit, double value)
        {
            return value * GetConversionFactor(unit);
        }

        /*
         * Converts BASE UNIT (Kilogram) value to target unit
         */
        public static double ConvertFromBaseUnit(WeightUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }
    }
}