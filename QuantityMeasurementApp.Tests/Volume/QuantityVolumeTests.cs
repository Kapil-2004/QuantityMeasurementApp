using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Volume
{
    [TestClass]
    public class QuantityVolumeTests
    {
        private const double EPSILON = 0.0001;

        /// <summary>
        /// 1 Litre = 1000 Millilitre
        /// </summary>
        [TestMethod]
        public void testEquality_LitreToMillilitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            Assert.IsTrue(q1.Equals(q2));
        }

        /// <summary>
        /// 1 Gallon ≈ 3.78541 Litre
        /// </summary>
        [TestMethod]
        public void testEquality_GallonToLitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
            var q2 = new Quantity<VolumeUnit>(3.78541, VolumeUnit.Litre);

            Assert.IsTrue(q1.Equals(q2));
        }

        /// <summary>
        /// Conversion test
        /// </summary>
        [TestMethod]
        public void testConversion_LitreToMillilitre()
        {
            var q = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);

            var result = q.ConvertTo(VolumeUnit.Millilitre);

            Assert.AreEqual(1000.0, result.Value, EPSILON);
        }

        /// <summary>
        /// Addition test
        /// </summary>
        [TestMethod]
        public void testAddition_LitrePlusMillilitre()
        {
            var q1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
            var q2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);

            var result = q1.Add(q2);

            Assert.AreEqual(2.0, result.Value, EPSILON);
            Assert.AreEqual(VolumeUnit.Litre, result.Unit);
        }
    }
}