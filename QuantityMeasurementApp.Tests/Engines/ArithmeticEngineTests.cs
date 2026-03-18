using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;

namespace QuantityMeasurementApp.Tests.Engines
{
    [TestClass]
    public class ArithmeticEngineTests
    {
        [TestMethod]
        public void Add_ShouldReturnCorrectResult()
        {
            double result = ArithmeticEngine.Add(10, 5, "Length");

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Subtract_ShouldReturnCorrectResult()
        {
            double result = ArithmeticEngine.Subtract(10, 5, "Weight");

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Divide_ShouldReturnCorrectResult()
        {
            double result = ArithmeticEngine.Divide(10, 5, "Volume");

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Divide_ByOne_ShouldReturnSameNumber()
        {
            double result = ArithmeticEngine.Divide(10, 1, "Length");

            Assert.AreEqual(10, result);
        }
    }
}