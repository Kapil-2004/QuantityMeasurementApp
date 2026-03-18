using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementApp.Tests.Engines
{
    [TestClass]
    public class ValidationEngineTests
    {
        [TestMethod]
        public void ValidateSameMeasurement_ShouldPass_ForSameCategory()
        {
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            
            ValidationEngine.ValidateSameMeasurement(q1, q2);

            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(QuantityMeasurementException))]
        public void ValidateSameMeasurement_ShouldThrowException_ForDifferentCategory()
        {
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(1, WeightUnit.Gram.ToString(), WeightUnit.Gram.GetMeasurementType());
            
            ValidationEngine.ValidateSameMeasurement(q1, q2);
        }
    }
}