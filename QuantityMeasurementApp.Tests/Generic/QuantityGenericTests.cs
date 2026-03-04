using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Generic
{
    [TestClass]
    public class QuantityGenericTests
    {
        private const double EPSILON = 0.0001;

        [TestMethod]
        public void GenericQuantity_ShouldSupport_Length()
        {
            var l1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var l2 = new Quantity<LengthUnit>(12, LengthUnit.Inch);

            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod]
        public void GenericQuantity_ShouldSupport_Weight()
        {
            var w1 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);
            var w2 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);

            Assert.IsTrue(w1.Equals(w2));
        }
    }
}