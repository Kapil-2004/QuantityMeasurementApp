using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestClass]
    public class QuantityVolumeServiceTests
    {
        private QuantityVolumeService _service;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityVolumeService();
        }

        /// <summary>
        /// Test equality using service
        /// </summary>
        [TestMethod]
        public void testEquality_LitreToMillilitre()
        {
            bool result = _service.AreEqual(
                1.0, VolumeUnit.Litre,
                1000.0, VolumeUnit.Millilitre);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test conversion
        /// </summary>
        [TestMethod]
        public void testConversion_GallonToLitre()
        {
            double result = _service.Convert(
                1.0,
                VolumeUnit.Gallon,
                VolumeUnit.Litre);

            Assert.AreEqual(3.78541, result, EPSILON);
        }

        /// <summary>
        /// Test addition
        /// </summary>
        [TestMethod]
        public void testAddition_LitrePlusMillilitre()
        {
            var result = _service.Add(
                1.0, VolumeUnit.Litre,
                1000.0, VolumeUnit.Millilitre);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(VolumeUnit.Litre, result.Unit);
        }

        /// <summary>
        /// Test addition with explicit target unit
        /// </summary>
        [TestMethod]
        public void testAddition_TargetUnit_Millilitre()
        {
            var result = _service.Add(
                1.0, VolumeUnit.Litre,
                1000.0, VolumeUnit.Millilitre,
                VolumeUnit.Millilitre);

            Assert.AreEqual(2000.0, result.Value, EPSILON);
            Assert.AreEqual(VolumeUnit.Millilitre, result.Unit);
        }
    }
}