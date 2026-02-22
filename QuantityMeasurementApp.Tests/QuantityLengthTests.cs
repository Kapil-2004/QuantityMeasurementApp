using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthTests
    {
        private QuantityLengthService _service;

        // Runs before each test method
        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        // Tests same Feet values
        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Feet,
                                            1.0, LengthUnit.Feet));
        }

        // Tests same Inch values
        [TestMethod]
        public void testEquality_InchToInch_SameValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Inch,
                                            1.0, LengthUnit.Inch));
        }

        // Tests cross-unit equality (1 Foot == 12 Inches)
        [TestMethod]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(1.0, LengthUnit.Feet,
                                            12.0, LengthUnit.Inch));
        }

        // Tests reverse cross-unit equality
        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            Assert.IsTrue(_service.AreEqual(12.0, LengthUnit.Inch,
                                            1.0, LengthUnit.Feet));
        }

        // Tests different values
        [TestMethod]
        public void testEquality_DifferentValue()
        {
            Assert.IsFalse(_service.AreEqual(1.0, LengthUnit.Feet,
                                             2.0, LengthUnit.Feet));
        }

        // Tests comparison with null
        [TestMethod]
        public void testEquality_NullComparison()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsFalse(q1.Equals(null));
        }

        // Tests reflexive property (object equals itself)
        [TestMethod]
        public void testEquality_SameReference()
        {
            QuantityLength q1 = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.IsTrue(q1.Equals(q1));
        }
    }
}