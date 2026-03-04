using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestClass]
    public class QuantityWeightServiceTests
    {
        private QuantityWeightService _service = null!;
        private const double EPSILON = 0.0001;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityWeightService();
        }

        [TestMethod]
        public void AreEqual_ShouldReturnTrue_ForEquivalentWeights()
        {
            var w1 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);
            var w2 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);

            Assert.IsTrue(_service.AreEqual(w1, w2));
        }

        [TestMethod]
        public void Add_ShouldReturnCorrectSum()
        {
            var w1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(500, WeightUnit.Gram);

            var result = _service.Add(w1, w2);

            Assert.AreEqual(1.5, result.Value, EPSILON);
            Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
        }
    }
}