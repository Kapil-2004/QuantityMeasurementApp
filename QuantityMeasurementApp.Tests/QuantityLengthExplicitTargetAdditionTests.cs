using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthExplicitTargetAdditionTests
    {
        private QuantityLengthService _service;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        // ============================================================
        // EXPLICIT TARGET UNIT TESTS
        // ============================================================

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            var result = _service.Add(1.0, LengthUnit.Feet,
                                      12.0, LengthUnit.Inch,
                                      LengthUnit.Feet);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            var result = _service.Add(1.0, LengthUnit.Feet,
                                      12.0, LengthUnit.Inch,
                                      LengthUnit.Inch);

            Assert.AreEqual(24.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            var result = _service.Add(1.0, LengthUnit.Feet,
                                      12.0, LengthUnit.Inch,
                                      LengthUnit.Yard);

            Assert.AreEqual(0.6667, result.Value, 0.001);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Centimeters()
        {
            var result = _service.Add(1.0, LengthUnit.Inch,
                                      1.0, LengthUnit.Inch,
                                      LengthUnit.Centimeter);

            Assert.AreEqual(5.08, result.Value, 0.01);
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }

        // ============================================================
        // COMMUTATIVITY WITH TARGET UNIT
        // ============================================================

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            var a = _service.Add(1.0, LengthUnit.Feet,
                                 12.0, LengthUnit.Inch,
                                 LengthUnit.Yard);

            var b = _service.Add(12.0, LengthUnit.Inch,
                                 1.0, LengthUnit.Feet,
                                 LengthUnit.Yard);

            Assert.AreEqual(a.Value, b.Value, EPSILON);
        }

        // ============================================================
        // NEGATIVE & ZERO
        // ============================================================

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_WithZero()
        {
            var result = _service.Add(5.0, LengthUnit.Feet,
                                      0.0, LengthUnit.Inch,
                                      LengthUnit.Yard);

            Assert.AreEqual(1.6667, result.Value, 0.001);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_NegativeValues()
        {
            var result = _service.Add(5.0, LengthUnit.Feet,
                                     -2.0, LengthUnit.Feet,
                                      LengthUnit.Inch);

            Assert.AreEqual(36.0, result.Value, EPSILON);
        }

        // ============================================================
        // INVALID TARGET UNIT
        // ============================================================

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_InvalidTarget()
        {
            try
            {
                _service.Add(1.0, LengthUnit.Feet,
                             12.0, LengthUnit.Inch,
                             (LengthUnit)999);

                Assert.Fail("Expected ArgumentException not thrown.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}