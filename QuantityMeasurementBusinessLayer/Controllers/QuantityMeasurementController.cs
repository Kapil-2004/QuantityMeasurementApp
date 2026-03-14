using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Controllers
{
    /// <summary>
    /// Business layer controller that handles user interactions and delegates to the service layer.
    /// Formats and displays operation results to the console.
    /// </summary>
    public class QuantityMeasurementController
    {
        /// <summary>Service instance providing business logic</summary>
        private readonly IQuantityMeasurementService service;

        /// <summary>Constructor accepting service dependency</summary>
        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Performs a comparison operation and displays the result.
        /// </summary>
        public void PerformComparison(QuantityDTO q1, QuantityDTO q2)
        {
            bool result = service.Compare(q1, q2);
            Console.WriteLine($"Comparison Result: {result}");
        }

        /// <summary>
        /// Performs a conversion operation and displays the converted value and unit.
        /// </summary>
        public void PerformConversion(QuantityDTO input, string targetUnit)
        {
            QuantityDTO result = service.Convert(input, targetUnit);
            Console.WriteLine($"{input.Value} {input.Unit} = {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Performs an addition operation and displays the result with unit.
        /// </summary>
        public void PerformAddition(QuantityDTO q1, QuantityDTO q2)
        {
            QuantityDTO result = service.Add(q1, q2);
            Console.WriteLine($"Addition Result: {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Performs a subtraction operation and displays the result with unit.
        /// </summary>
        public void PerformSubtraction(QuantityDTO q1, QuantityDTO q2)
        {
            QuantityDTO result = service.Subtract(q1, q2);
            Console.WriteLine($"Subtraction Result: {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Performs a division operation and displays the dimensionless result.
        /// </summary>
        public void PerformDivision(QuantityDTO q1, QuantityDTO q2)
        {
            double result = service.Divide(q1, q2);
            Console.WriteLine($"Division Result: {result}");
        }
    }
}