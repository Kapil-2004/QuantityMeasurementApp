using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;

namespace QuantityMeasurementBusinessLayer.Controllers
{
    public class QuantityMeasurementController
    {
        private readonly IQuantityMeasurementService service;

        public QuantityMeasurementController(IQuantityMeasurementService service)
        {
            this.service = service;
        }

        public void PerformComparison(QuantityDTO q1, QuantityDTO q2)
        {
            bool result = service.Compare(q1, q2);

            Console.WriteLine($"Comparison Result: {result}");
        }

        public void PerformConversion(QuantityDTO input, string targetUnit)
        {
            QuantityDTO result = service.Convert(input, targetUnit);

            Console.WriteLine($"{input.Value} {input.Unit} = {result.Value} {result.Unit}");
        }

        public void PerformAddition(QuantityDTO q1, QuantityDTO q2)
        {
            QuantityDTO result = service.Add(q1, q2);

            Console.WriteLine($"Addition Result: {result.Value} {result.Unit}");
        }

        public void PerformSubtraction(QuantityDTO q1, QuantityDTO q2)
        {
            QuantityDTO result = service.Subtract(q1, q2);

            Console.WriteLine($"Subtraction Result: {result.Value} {result.Unit}");
        }

        public void PerformDivision(QuantityDTO q1, QuantityDTO q2)
        {
            double result = service.Divide(q1, q2);

            Console.WriteLine($"Division Result: {result}");
        }
    }
}