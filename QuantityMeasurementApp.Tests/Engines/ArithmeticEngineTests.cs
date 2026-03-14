using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementBusinessLayer.Engines;

namespace QuantityMeasurementApp.Tests.Engines
{
    /// <summary>
    /// Unit tests for ArithmeticEngine static utility validating arithmetic operations on base units.
    /// Tests Add, Subtract, and Divide operations with various measurement types.
    /// Ensures correct arithmetic on normalized values and proper divisor validation.
    /// </summary>
    [TestClass]
    public class ArithmeticEngineTests
    {
        /// <summary>
        /// TEST: Add should return sum of two base unit values.
        /// SCENARIO: 10 + 5 with measurement type Length
        /// EXPECTED: Returns 15
        /// PURPOSE: Verify basic addition operation on normalized base values
        /// </summary>
        [TestMethod]
        public void Add_ShouldReturnCorrectResult()
        {
            // Act: Add two base unit values
            double result = ArithmeticEngine.Add(10, 5, "Length");
            // Assert: 10 + 5 should equal 15
            Assert.AreEqual(15, result);
        }

        /// <summary>
        /// TEST: Subtract should return difference of two base unit values.
        /// SCENARIO: 10 - 5 with measurement type Weight
        /// EXPECTED: Returns 5
        /// PURPOSE: Verify basic subtraction operation on normalized base values
        /// </summary>
        [TestMethod]
        public void Subtract_ShouldReturnCorrectResult()
        {
            // Act: Subtract second base value from first
            double result = ArithmeticEngine.Subtract(10, 5, "Weight");
            // Assert: 10 - 5 should equal 5
            Assert.AreEqual(5, result);
        }

        /// <summary>
        /// TEST: Divide should return quotient of two base unit values.
        /// SCENARIO: 10 ÷ 5 with measurement type Volume
        /// EXPECTED: Returns 2
        /// PURPOSE: Verify basic division operation on normalized base values
        /// </summary>
        [TestMethod]
        public void Divide_ShouldReturnCorrectResult()
        {
            // Act: Divide first base value by second
            double result = ArithmeticEngine.Divide(10, 5, "Volume");
            // Assert: 10 ÷ 5 should equal 2
            Assert.AreEqual(2, result);
        }

        /// <summary>
        /// TEST: Divide by 1 should return the original number unchanged.
        /// SCENARIO: 10 ÷ 1 with measurement type Length
        /// EXPECTED: Returns 10 (any number divided by 1 equals itself)
        /// PURPOSE: Verify edge case of dividing by one
        /// </summary>
        [TestMethod]
        public void Divide_ByOne_ShouldReturnSameNumber()
        {
            // Act: Divide base value by 1
            double result = ArithmeticEngine.Divide(10, 1, "Length");
            // Assert: 10 ÷ 1 should equal 10
            Assert.AreEqual(10, result);
        }
    }
}