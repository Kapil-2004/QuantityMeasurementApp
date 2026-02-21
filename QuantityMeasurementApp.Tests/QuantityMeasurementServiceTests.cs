using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _service;

        // Runs before each test
        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        // Test: 1.0 ft == 1.0 ft
        [TestMethod]
        public void testEquality_SameValue()
        {
            Feet first = new Feet(1.0);
            Feet second = new Feet(1.0);

            bool result = _service.AreEqual(first, second);

            Assert.IsTrue(result, "1.0 ft should be equal to 1.0 ft");
        }

        // Test: 1.0 ft != 2.0 ft
        [TestMethod]
        public void testEquality_DifferentValue()
        {
            Feet first = new Feet(1.0);
            Feet second = new Feet(2.0);

            bool result = _service.AreEqual(first, second);

            Assert.IsFalse(result, "1.0 ft should NOT be equal to 2.0 ft");
        }

        // Test: value compared with null
        [TestMethod]
        public void testEquality_NullComparison()
        {
            Feet first = new Feet(1.0);

            bool result = _service.AreEqual(first, null);

            Assert.IsFalse(result, "Feet should not be equal to null");
        }

        // Test: non-numeric input handling
        [TestMethod]
        public void testEquality_NonNumericInput()
        {
            Feet first = _service.CreateFeet("1.0");
            Feet second = _service.CreateFeet("abc");

            bool result = _service.AreEqual(first, second);

            Assert.IsFalse(result, "Non numeric input should return false");
        }

        // Test: reflexive property (same object)
        [TestMethod]
        public void testEquality_SameReference()
        {
            Feet first = new Feet(1.0);

            bool result = _service.AreEqual(first, first);

            Assert.IsTrue(result, "Object should be equal to itself");
        }
    }
}