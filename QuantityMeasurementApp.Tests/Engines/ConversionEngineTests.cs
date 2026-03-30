using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;

namespace QuantityMeasurementApp.Tests.Engines
{
    [TestClass]
    public class ConversionEngineTests
    {
        [TestMethod]
        public void ConvertToBase_ShouldConvertFeetToInch()
        {
            var dto = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            double result = ConversionEngine.ConvertToBase(dto);

            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void ConvertToBase_ShouldConvertYardToInch()
        {
            var dto = new QuantityDTO(1, LengthUnit.Yard.ToString(), LengthUnit.Yard.GetMeasurementType());
            double result = ConversionEngine.ConvertToBase(dto);

            Assert.AreEqual(36, result);
        }

        [TestMethod]
        public void ConvertToBase_ShouldConvertKilogramToGram()
        {
            var dto = new QuantityDTO(1, WeightUnit.Kilogram.ToString(), WeightUnit.Kilogram.GetMeasurementType());
            double result = ConversionEngine.ConvertToBase(dto);

            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void ConvertToBase_ShouldConvertLiterToMilliliter()
        {
            var dto = new QuantityDTO(1, VolumeUnit.Litre.ToString(), VolumeUnit.Litre.GetMeasurementType());
            double result = ConversionEngine.ConvertToBase(dto);

            Assert.AreEqual(1000, result);
        }

        [TestMethod]
        public void ConvertFromBase_ShouldConvertInchToFeet()
        {
            double result = ConversionEngine.ConvertFromBase("Length", LengthUnit.Feet.ToString(), 12);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ConvertFromBase_ShouldConvertGramToKilogram()
        {
            double result = ConversionEngine.ConvertFromBase("Weight", WeightUnit.Kilogram.ToString(), 1000);

            Assert.AreEqual(1, result);
        }
    }
}
