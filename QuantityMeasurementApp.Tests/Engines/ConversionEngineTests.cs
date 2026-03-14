using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementApp.Tests.Engines
{
    /// <summary>
    /// Unit tests for ConversionEngine static utility validating base unit conversion logic.
    /// Tests both ToBase (convert to base unit) and FromBase (convert from base unit) operations.
    /// Ensures accurate conversions across all measurement types (Length, Weight, Volume, Temperature).
    /// </summary>
    [TestClass]
    public class ConversionEngineTests
    {
        /// <summary>
        /// TEST: ConvertToBase should convert Feet (length) to base unit (Inch).
        /// SCENARIO: 1 Foot input
        /// EXPECTED: Returns 12 (since 1 Foot = 12 Inches)
        /// PURPOSE: Verify length conversion factor Feet→Inch
        /// </summary>
        [TestMethod]
        public void ConvertToBase_ShouldConvertFeetToInch()
        {
            // Arrange: Create 1 Foot quantity
            var dto = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            // Act: Convert to base unit (Inch)
            double result = ConversionEngine.ConvertToBase(dto);
            // Assert: 1 Foot should equal 12 Inches
            Assert.AreEqual(12, result);
        }

        /// <summary>
        /// TEST: ConvertToBase should convert Yard (length) to base unit (Inch).
        /// SCENARIO: 1 Yard input
        /// EXPECTED: Returns 36 (since 1 Yard = 36 Inches)
        /// PURPOSE: Verify length conversion factor Yard→Inch
        /// </summary>
        [TestMethod]
        public void ConvertToBase_ShouldConvertYardToInch()
        {
            // Arrange: Create 1 Yard quantity
            var dto = new QuantityDTO(1, LengthUnit.Yard.ToString(), LengthUnit.Yard.GetMeasurementType());
            // Act: Convert to base unit (Inch)
            double result = ConversionEngine.ConvertToBase(dto);
            // Assert: 1 Yard should equal 36 Inches
            Assert.AreEqual(36, result);
        }

        /// <summary>
        /// TEST: ConvertToBase should convert Kilogram (weight) to base unit (Gram).
        /// SCENARIO: 1 Kilogram input
        /// EXPECTED: Returns 1000 (since 1 Kilogram = 1000 Grams)
        /// PURPOSE: Verify weight conversion factor Kilogram→Gram
        /// </summary>
        [TestMethod]
        public void ConvertToBase_ShouldConvertKilogramToGram()
        {
            // Arrange: Create 1 Kilogram quantity
            var dto = new QuantityDTO(1, WeightUnit.Kilogram.ToString(), WeightUnit.Kilogram.GetMeasurementType());
            // Act: Convert to base unit (Gram)
            double result = ConversionEngine.ConvertToBase(dto);
            // Assert: 1 Kilogram should equal 1000 Grams
            Assert.AreEqual(1000, result);
        }

        /// <summary>
        /// TEST: ConvertToBase should convert Litre (volume) to base unit (Millilitre).
        /// SCENARIO: 1 Litre input
        /// EXPECTED: Returns 1000 (since 1 Litre = 1000 Millilitres)
        /// PURPOSE: Verify volume conversion factor Litre→Millilitre
        /// </summary>
        [TestMethod]
        public void ConvertToBase_ShouldConvertLiterToMilliliter()
        {
            // Arrange: Create 1 Litre quantity
            var dto = new QuantityDTO(1, VolumeUnit.Litre.ToString(), VolumeUnit.Litre.GetMeasurementType());
            // Act: Convert to base unit (Millilitre)
            double result = ConversionEngine.ConvertToBase(dto);
            // Assert: 1 Litre should equal 1000 Millilitres
            Assert.AreEqual(1000, result);
        }

        /// <summary>
        /// TEST: ConvertFromBase should convert 12 Inches (base) to Feet.
        /// SCENARIO: Convert 12 base units (Inches) to target unit Feet
        /// EXPECTED: Returns 1 (since 12 Inches = 1 Foot)
        /// PURPOSE: Verify reverse conversion from base unit to specific unit
        /// </summary>
        [TestMethod]
        public void ConvertFromBase_ShouldConvertInchToFeet()
        {
            // Act: Convert 12 base units (Inches) to Feet
            double result = ConversionEngine.ConvertFromBase("Length", LengthUnit.Feet.ToString(), 12);
            // Assert: 12 Inches should convert back to 1 Foot
            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// TEST: ConvertFromBase should convert 1000 Grams (base) to Kilogram.
        /// SCENARIO: Convert 1000 base units (Grams) to target unit Kilogram
        /// EXPECTED: Returns 1 (since 1000 Grams = 1 Kilogram)
        /// PURPOSE: Verify reverse conversion from base unit to specific unit
        /// </summary>
        [TestMethod]
        public void ConvertFromBase_ShouldConvertGramToKilogram()
        {
            // Act: Convert 1000 base units (Grams) to Kilogram
            double result = ConversionEngine.ConvertFromBase("Weight", WeightUnit.Kilogram.ToString(), 1000);
            // Assert: 1000 Grams should convert back to 1 Kilogram
            Assert.AreEqual(1, result);
        }
    }
}