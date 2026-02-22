using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthTests
    {
        private QuantityLengthService _service;

        // Runs before each test
        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        // ============================================================
        // FEET TESTS
        // ============================================================

        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Feet,
                                            1.0, LengthUnit.Feet));
        }

        [TestMethod]
        public void testEquality_FeetToFeet_DifferentValue()
        {
            Assert.IsFalse(_service.AreEqual(1.0, LengthUnit.Feet,
                                             2.0, LengthUnit.Feet));
        }

        // ============================================================
        // INCH TESTS
        // ============================================================

        [TestMethod]
        public void testEquality_InchToInch_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(5.0, LengthUnit.Inch,
                                            5.0, LengthUnit.Inch));
        }

        [TestMethod]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Feet,
                                            12.0, LengthUnit.Inch));
        }

        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(12.0, LengthUnit.Inch,
                                            1.0, LengthUnit.Feet));
        }

        // ============================================================
        // YARD TESTS
        // ============================================================

        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(2.0, LengthUnit.Yard,
                                            2.0, LengthUnit.Yard));
        }

        [TestMethod]
        public void testEquality_YardToYard_DifferentValue()
        {
            Assert.IsFalse(_service.AreEqual(1.0, LengthUnit.Yard,
                                             2.0, LengthUnit.Yard));
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Yard,
                                            3.0, LengthUnit.Feet));
        }

        [TestMethod]
        public void testEquality_FeetToYard_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(3.0, LengthUnit.Feet,
                                            1.0, LengthUnit.Yard));
        }

        [TestMethod]
        public void testEquality_YardToInch_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Yard,
                                            36.0, LengthUnit.Inch));
        }

        [TestMethod]
        public void testEquality_YardToFeet_NonEquivalentValue()
        {
            Assert.IsFalse(_service.AreEqual(1.0, LengthUnit.Yard,
                                             2.0, LengthUnit.Feet));
        }

        // ============================================================
        // CENTIMETER TESTS
        // ============================================================

        [TestMethod]
        public void testEquality_CentimeterToCentimeter_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(5.0, LengthUnit.Centimeter,
                                            5.0, LengthUnit.Centimeter));
        }

        [TestMethod]
        public void testEquality_CentimeterToInch_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Centimeter,
                                            0.393701, LengthUnit.Inch));
        }

        [TestMethod]
        public void testEquality_CentimeterToFeet_NonEquivalentValue()
        {
            Assert.IsFalse(_service.AreEqual(1.0, LengthUnit.Centimeter,
                                             1.0, LengthUnit.Feet));
        }

        // ============================================================
        // MULTI-UNIT COMPLEX SCENARIOS
        // ============================================================

        [TestMethod]
        public void testEquality_AllUnits_ComplexScenario()
        {
            Assert.IsTrue(_service.AreEqual(2.0, LengthUnit.Yard,
                                            6.0, LengthUnit.Feet));

            Assert.IsTrue(_service.AreEqual(6.0, LengthUnit.Feet,
                                            72.0, LengthUnit.Inch));
        }

        [TestMethod]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            QuantityLength yard = new QuantityLength(1.0, LengthUnit.Yard);
            QuantityLength feet = new QuantityLength(3.0, LengthUnit.Feet);
            QuantityLength inch = new QuantityLength(36.0, LengthUnit.Inch);

            Assert.IsTrue(yard.Equals(feet));
            Assert.IsTrue(feet.Equals(inch));
            Assert.IsTrue(yard.Equals(inch));
        }

        // ============================================================
        // NULL & REFERENCE TESTS
        // ============================================================

        [TestMethod]
        public void testEquality_SameReference()
        {
            QuantityLength q = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q.Equals(q));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            QuantityLength q = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(q.Equals(null));
        }
    }
}