using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementRepositoryLayer.Implementations;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private IQuantityMeasurementService _service;

        [TestInitialize]
        public void Setup()
        {
            IQuantityMeasurementRepository repository =
                QuantityMeasurementCacheRepository.GetInstance();

            _service = new QuantityMeasurementServiceImpl(repository);
        }

        [TestMethod]
        public void Compare_ShouldReturnTrue_ForEqualLength()
        {
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());

            var result = _service.Compare(q1, q2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_ShouldReturnCorrectLength()
        {
            var q1 = new QuantityDTO(1, LengthUnit.Feet.ToString(), LengthUnit.Feet.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());

            var result = _service.Add(q1, q2);

            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void Subtract_ShouldReturnCorrectLength()
        {
            var q1 = new QuantityDTO(24, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());

            var result = _service.Subtract(q1, q2);

            Assert.AreEqual(12, result.Value);
        }

        [TestMethod]
        public void Divide_ShouldReturnCorrectResult()
        {
            var q1 = new QuantityDTO(24, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());
            var q2 = new QuantityDTO(12, LengthUnit.Inch.ToString(), LengthUnit.Inch.GetMeasurementType());

            var result = _service.Divide(q1, q2);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Convert_ShouldConvertLiterToMilliliter()
        {
            var quantity = new QuantityDTO(1, VolumeUnit.Litre.ToString(), VolumeUnit.Litre.GetMeasurementType());

            var result = _service.Convert(quantity, VolumeUnit.Millilitre.ToString());

            Assert.AreEqual(1000, result.Value);
        }
    }
}
