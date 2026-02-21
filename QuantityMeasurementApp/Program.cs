using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QuantityMeasurementService service = new QuantityMeasurementService();

            Console.WriteLine("Enter first value in feet:");
            string input1 = Console.ReadLine();

            Console.WriteLine("Enter second value in feet:");
            string input2 = Console.ReadLine();

            // Convert inputs to Feet objects
            Feet feet1 = service.CreateFeet(input1);
            Feet feet2 = service.CreateFeet(input2);

            // Validate numeric input
            if (feet1 == null || feet2 == null)
            {
                Console.WriteLine("Invalid numeric input.");
                return;
            }

            // Compare values
            bool result = service.AreEqual(feet1, feet2);

            Console.WriteLine($"Equal ({result})");
        }
    }
}