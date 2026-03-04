using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;
using System;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthConversionTests
    {
        private QuantityLengthService _service;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        // ============================================================
        // BASIC CONVERSIONS
        // ============================================================

        [TestMethod]
        public void testConversion_FeetToInch()
        {
            double result = _service.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(12.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_InchToFeet()
        {
            double result = _service.Convert(24.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_YardToInch()
        {
            double result = _service.Convert(1.0, LengthUnit.Yard, LengthUnit.Inch);
            Assert.AreEqual(36.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_InchToYard()
        {
            double result = _service.Convert(72.0, LengthUnit.Inch, LengthUnit.Yard);
            Assert.AreEqual(2.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_CentimeterToInch()
        {
            double result = _service.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
            Assert.AreEqual(1.0, result, EPSILON);
        }

        // ============================================================
        // EDGE CASES
        // ============================================================

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            double result = _service.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(0.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            double result = _service.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(-12.0, result, EPSILON);
        }

        [TestMethod]
        public void testConversion_SameUnit()
        {
            double result = _service.Convert(5.0, LengthUnit.Feet, LengthUnit.Feet);
            Assert.AreEqual(5.0, result, EPSILON);
        }

        // ============================================================
        // ROUND TRIP
        // ============================================================

        [TestMethod]
        public void testConversion_RoundTrip()
        {
            double value = 10.0;

            double converted = _service.Convert(value, LengthUnit.Feet, LengthUnit.Inch);
            double back = _service.Convert(converted, LengthUnit.Inch, LengthUnit.Feet);

            Assert.AreEqual(value, back, EPSILON);
        }

        // ============================================================
        // INVALID INPUT
        // ============================================================

        [TestMethod]
        public void testConversion_NaN_Throws()
        {
            try
            {
                _service.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inch);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true); // Test passed
            }
        }

        [TestMethod]
        public void testConversion_Infinity_Throws()
        {
            try
            {
                _service.Convert(double.PositiveInfinity, LengthUnit.Feet, LengthUnit.Inch);
                Assert.Fail("Expected ArgumentException was not thrown.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true); // Test passed
            }
        }
    }
}