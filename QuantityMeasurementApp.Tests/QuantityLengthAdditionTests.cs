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

        [TestMethod]
        public void testAddition_CentimeterPlusInch()
        {
            var result = _service.Add(2.54, LengthUnit.Centimeter,
                                      1.0, LengthUnit.Inch);

            Assert.AreEqual(5.08, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
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

            // Convert b result to feet for proper comparison
            double bInFeet = b.Value * b.Unit.ToFeetFactor();

            Assert.AreEqual(a.Value, bInFeet, EPSILON);
        }

        // ============================================================
        // INVALID VALUE TEST
        // ============================================================

        [TestMethod]
        public void testAddition_InvalidNumericValue()
        {
            try
            {
                _service.Add(double.NaN, LengthUnit.Feet,
                            2.0, LengthUnit.Feet);

                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                // Correct exception type
            }
            catch (Exception ex)
            {
                Assert.Fail($"Wrong exception type thrown: {ex.GetType()}");
            }
        }

        // ============================================================
        // LARGE & SMALL VALUES
        // ============================================================

        [TestMethod]
        public void testAddition_LargeValues()
        {
            var result = _service.Add(1e6, LengthUnit.Feet,
                                      1e6, LengthUnit.Feet);

            Assert.AreEqual(2e6, result.Value, EPSILON);
        }

        [TestMethod]
        public void testAddition_SmallValues()
        {
            var result = _service.Add(0.001, LengthUnit.Feet,
                                      0.002, LengthUnit.Feet);

            Assert.AreEqual(0.003, result.Value, EPSILON);
        }
    }
}