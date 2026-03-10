// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using QuantityMeasurementApp.Models;


// namespace QuantityMeasurementApp.Tests.Temperature
// {
//     [TestClass]
//     public class QuantityTemperatureTests
//     {
//         private const double EPSILON = 0.01;

//         [TestMethod]
//         public void Celsius_To_Fahrenheit_Conversion()
//         {
//             var temp = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);

//             var result = temp.ConvertTo(TemperatureUnit.FAHRENHEIT);

//             Assert.AreEqual(32, result.Value, EPSILON);
//         }

//         [TestMethod]
//         public void Fahrenheit_To_Celsius_Conversion()
//         {
//             var temp = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

//             var result = temp.ConvertTo(TemperatureUnit.CELSIUS);

//             Assert.AreEqual(0, result.Value, EPSILON);
//         }

//         [TestMethod]
//         public void Celsius_To_Kelvin_Conversion()
//         {
//             var temp = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);

//             var result = temp.ConvertTo(TemperatureUnit.KELVIN);

//             Assert.AreEqual(273.15, result.Value, EPSILON);
//         }

//         [TestMethod]
//         public void Temperature_Addition_Should_Throw_Exception()
//         {
//             var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
//             var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

//             try
//             {
//                 t1.Add(t2);
//                 Assert.Fail("Expected UnsupportedOperationException was not thrown.");
//             }
//             catch (UnsupportedOperationException)
//             {
//             }
//         }

//         [TestMethod]
//         public void Temperature_Subtraction_Should_Throw_Exception()
//         {
//             var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
//             var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

//             try
//             {
//                 t1.Subtract(t2);
//                 Assert.Fail("Expected UnsupportedOperationException was not thrown.");
//             }
//             catch (UnsupportedOperationException)
//             {
//             }
//         }

//         [TestMethod]
//         public void Temperature_Division_Should_Throw_Exception()
//         {
//             var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
//             var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

//             try
//             {
//                 t1.Divide(t2);
//                 Assert.Fail("Expected UnsupportedOperationException was not thrown.");
//             }
//             catch (UnsupportedOperationException)
//             {
//             }
//         }
//     }
// }