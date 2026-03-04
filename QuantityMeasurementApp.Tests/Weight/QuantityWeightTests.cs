using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityWeightTests
    {
        private QuantityWeightService _service;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityWeightService();
        }

        // ==========================================================
        // EQUALITY TESTS
        // ==========================================================

        [TestMethod]
        public void testEquality_KilogramToGram()
        {
            bool result = _service.AreEqual(1.0, WeightUnit.Kilogram,
                                            1000.0, WeightUnit.Gram);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_KilogramToPound()
        {
            bool result = _service.AreEqual(1.0, WeightUnit.Kilogram,
                                            2.20462, WeightUnit.Pound);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testEquality_DifferentValues()
        {
            bool result = _service.AreEqual(1.0, WeightUnit.Kilogram,
                                            2.0, WeightUnit.Kilogram);

            Assert.IsFalse(result);
        }

        // ==========================================================
        // CONVERSION TESTS
        // ==========================================================

        [TestMethod]
        public void testConversion_KilogramToGram()
        {
            double result = _service.Convert(1.0, WeightUnit.Kilogram,
                                             WeightUnit.Gram);

            Assert.AreEqual(1000.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_PoundToKilogram()
        {
            double result = _service.Convert(2.20462, WeightUnit.Pound,
                                             WeightUnit.Kilogram);

            Assert.AreEqual(1.0, result, 0.01);
        }

        // ==========================================================
        // ADDITION TESTS
        // ==========================================================

        [TestMethod]
        public void testAddition_KilogramPlusGram()
        {
            var result = _service.Add(1.0, WeightUnit.Kilogram,
                                      1000.0, WeightUnit.Gram);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTarget_Gram()
        {
            var result = _service.Add(1.0, WeightUnit.Kilogram,
                                      1000.0, WeightUnit.Gram,
                                      WeightUnit.Gram);

            Assert.AreEqual(2000.0, result.Value, EPSILON);
            Assert.AreEqual(WeightUnit.Gram, result.Unit);
        }

        [TestMethod]
        public void testAddition_Negative()
        {
            var result = _service.Add(5.0, WeightUnit.Kilogram,
                                     -2000.0, WeightUnit.Gram);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }
    }
}