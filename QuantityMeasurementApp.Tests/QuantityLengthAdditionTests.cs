using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthAdditionTests
    {
        private QuantityLengthService _service;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        // ============================================================
        // SAME UNIT ADDITION
        // ============================================================

        [TestMethod]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            var result = _service.Add(1.0, LengthUnit.Feet,
                                      2.0, LengthUnit.Feet);

            Assert.AreEqual(3.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_SameUnit_InchPlusInch()
        {
            var result = _service.Add(6.0, LengthUnit.Inch,
                                      6.0, LengthUnit.Inch);

            Assert.AreEqual(12.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        // ============================================================
        // CROSS UNIT ADDITION
        // ============================================================

        [TestMethod]
        public void testAddition_FeetPlusInches()
        {
            var result = _service.Add(1.0, LengthUnit.Feet,
                                      12.0, LengthUnit.Inch);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_InchesPlusFeet()
        {
            var result = _service.Add(12.0, LengthUnit.Inch,
                                      1.0, LengthUnit.Feet);

            Assert.AreEqual(24.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_YardPlusFeet()
        {
            var result = _service.Add(1.0, LengthUnit.Yard,
                                      3.0, LengthUnit.Feet);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        // ============================================================
        // IDENTITY & NEGATIVE
        // ============================================================

        [TestMethod]
        public void testAddition_WithZero()
        {
            var result = _service.Add(5.0, LengthUnit.Feet,
                                      0.0, LengthUnit.Inch);

            Assert.AreEqual(5.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_NegativeValue()
        {
            var result = _service.Add(5.0, LengthUnit.Feet,
                                     -2.0, LengthUnit.Feet);

            Assert.AreEqual(3.0, result.Value, EPSILON);
        }

        // ============================================================
        // COMMUTATIVITY
        // ============================================================

        [TestMethod]
        public void testAddition_Commutativity()
        {
            var a = _service.Add(1.0, LengthUnit.Feet,
                                 12.0, LengthUnit.Inch);

            var b = _service.Add(12.0, LengthUnit.Inch,
                                 1.0, LengthUnit.Feet);

            // Convert b to feet for comparison
            double bInFeet = b.Value * b.Unit.ToFeetFactor();

            Assert.AreEqual(a.Value, bInFeet, EPSILON);
        }
    }
}