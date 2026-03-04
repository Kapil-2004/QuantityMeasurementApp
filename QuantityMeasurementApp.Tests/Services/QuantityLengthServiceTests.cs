using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestClass]
    public class QuantityLengthServiceTests
    {
        private QuantityLengthService _service = null!;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityLengthService();
        }

        [TestMethod]
        public void AreEqual_ShouldReturnTrue_ForEquivalentLengths()
        {
            var l1 = new Quantity<LengthUnit>(12, LengthUnit.Inch);
            var l2 = new Quantity<LengthUnit>(1, LengthUnit.Feet);

            Assert.IsTrue(_service.AreEqual(l1, l2));
        }

        [TestMethod]
        public void Add_ShouldReturnCorrectSum_InFirstUnit()
        {
            var l1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var l2 = new Quantity<LengthUnit>(12, LengthUnit.Inch);

            var result = _service.Add(l1, l2);

            Assert.AreEqual(2, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void Add_WithTargetUnit_ShouldReturnCorrectUnit()
        {
            var l1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var l2 = new Quantity<LengthUnit>(12, LengthUnit.Inch);

            var result = _service.Add(l1, l2, LengthUnit.Inch);

            Assert.AreEqual(24, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }
    }
}