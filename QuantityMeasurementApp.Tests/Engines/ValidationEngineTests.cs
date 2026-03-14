using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementApp.Tests.Engines
{
    /// <summary>
    /// Unit tests for ValidationEngine static utility validating measurement type compatibility.
    /// Ensures ValidateSameMeasurement correctly validates same-category measurements and rejects mixed types.
    /// </summary>
    [TestClass]
    public class ValidationEngineTests
    {
        /// <summary>
        /// TEST: ValidateSameMeasurement should accept two quantities of same measurement type but different units.
        /// SCENARIO: 1 Feet and 12 Inch (both Length measurements but different units)
        /// EXPECTED: No exception thrown, validation passes
        /// PURPOSE: Verify that validation correctly allows same-category quantities (Length vs Length)
        /// </summary>
        [TestMethod]
        public void ValidateSameMeasurement_ShouldPass_ForSameCategory()
        {
            // Arrange: Create two length quantities with different units
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            
            // Act: Validate same measurement type
            ValidationEngine.ValidateSameMeasurement(q1, q2);

            // Assert: If no exception thrown, validation passed
            Assert.IsTrue(true);
        }

        /// <summary>
        /// TEST: ValidateSameMeasurement should reject quantities of different measurement types.
        /// SCENARIO: 1 Foot (Length) and 1 Gram (Weight) - incompatible categories
        /// EXPECTED: QuantityMeasurementException thrown
        /// PURPOSE: Verify that validation correctly rejects mixed-type quantities (cannot add Length + Weight)
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(QuantityMeasurementException))]
        public void ValidateSameMeasurement_ShouldThrowException_ForDifferentCategory()
        {
            // Arrange: Create quantities of different measurement types
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(1, WeightUnit.Gram.ToString(), WeightUnit.Gram.GetMeasurementType());
            
            // Act: Attempt to validate incompatible types - should throw exception
            ValidationEngine.ValidateSameMeasurement(q1, q2);
            // Assert: Exception handled by [ExpectedException] attribute
        }
    }
}