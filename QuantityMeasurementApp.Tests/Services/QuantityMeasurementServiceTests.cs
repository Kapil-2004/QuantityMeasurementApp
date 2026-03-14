using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer.Implementations;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementApp.Tests.Services
{
    /// <summary>
    /// Integration tests for QuantityMeasurementService verifying end-to-end business logic.
    /// Tests service orchestration: validation, conversion, arithmetic, and persistence.
    /// Each test represents a complete workflow from DTO input to repository persistence.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        /// <summary>Service instance for testing business operations</summary>
        private IQuantityMeasurementService _service;

        /// <summary>
        /// Test setup: Initialize service with repository dependency (Singleton cache).
        /// Called before each test method to ensure clean state.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Get singleton repository instance (in-memory cache storage)
            IQuantityMeasurementRepository repository =
                QuantityMeasurementCacheRepository.GetInstance();

            // Inject repository into service
            _service = new QuantityMeasurementServiceImpl(repository);
        }

        /// <summary>
        /// TEST: Service should compare two equal lengths in different units.
        /// SCENARIO: Compare 1 Foot (12 Inches) with 12 Inches
        /// EXPECTED: Returns true (quantities are equal after normalization)
        /// WORKFLOW: Validate types → Convert to base → Compare → Save entity
        /// </summary>
        [TestMethod]
        public void Compare_ShouldReturnTrue_ForEqualLength()
        {
            // Arrange: Create two length quantities (1 Foot = 12 Inches)
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            // Act: Compare quantities
            var result = _service.Compare(q1, q2);
            // Assert: Should be equal when normalized to base unit
            Assert.IsTrue(result);
        }

        /// <summary>
        /// TEST: Service should add two quantities in different units and return result in first quantity's unit.
        /// SCENARIO: 1 Foot + 12 Inch
        /// EXPECTED: Returns 2 Feet (1 Foot + 1 Foot = 2 Feet)
        /// WORKFLOW: Validate types → Convert both to base → Add → Convert back → Save entity
        /// </summary>
        [TestMethod]
        public void Add_ShouldReturnCorrectLength()
        {
            // Arrange: Create two quantities to add (1 Foot + 12 Inches = 2 Feet)
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            // Act: Add quantities
            var result = _service.Add(q1, q2);
            // Assert: Result should be 2 in original unit (Feet)
            Assert.AreEqual(2, result.Value);
        }

        /// <summary>
        /// TEST: Service should subtract two quantities and return result in first quantity's unit.
        /// SCENARIO: 24 Inches - 12 Inches
        /// EXPECTED: Returns 12 Inches
        /// WORKFLOW: Validate types → Convert both to base → Subtract → Convert back → Save entity
        /// </summary>
        [TestMethod]
        public void Subtract_ShouldReturnCorrectLength()
        {
            // Arrange: Create two quantities to subtract (24 Inches - 12 Inches = 12 Inches)
            var q1 = new QuantityDTO(24, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            // Act: Subtract quantities
            var result = _service.Subtract(q1, q2);
            // Assert: Result should be 12
            Assert.AreEqual(12, result.Value);
        }

        /// <summary>
        /// TEST: Service should divide two quantities and return unitless ratio.
        /// SCENARIO: 24 Inches ÷ 12 Inches
        /// EXPECTED: Returns 2 (ratio, dimensionless)
        /// WORKFLOW: Validate types → Validate divisor non-zero → Convert both to base → Divide → Save entity
        /// </summary>
        [TestMethod]
        public void Divide_ShouldReturnCorrectResult()
        {
            // Arrange: Create two quantities to divide (24 Inches ÷ 12 Inches = 2)
            var q1 = new QuantityDTO(24, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            // Act: Divide quantities
            var result = _service.Divide(q1, q2);
            // Assert: Result should be unitless ratio 2
            Assert.AreEqual(2, result);
        }

        /// <summary>
        /// TEST: Service should convert quantity to different unit of same measurement type.
        /// SCENARIO: Convert 1 Litre to Millilitres
        /// EXPECTED: Returns 1000 Millilitres (1 Litre = 1000 Millilitres)
        /// WORKFLOW: Validate type → Convert to base → Convert from base to target unit → Save entity
        /// </summary>
        [TestMethod]
        public void Convert_ShouldConvertLiterToMilliliter()
        {
            // Arrange: Create volume quantity to convert
            var quantity = new QuantityDTO(1, VolumeUnit.Litre.ToString(), VolumeUnit.Litre.GetMeasurementType());
            // Act: Convert to different unit
            var result = _service.Convert(quantity, VolumeUnit.Millilitre.ToString());
            // Assert: 1 Litre should equal 1000 Millilitres
            Assert.AreEqual(1000, result.Value);
        }
    }
}