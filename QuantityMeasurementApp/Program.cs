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

            // --- UC1 Feet Equality Check ---
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

            // --- UC2 Inches Equality Check ---
            Console.WriteLine("Enter first value in inches:");
            string inchInput1 = Console.ReadLine();

            Console.WriteLine("Enter second value in inches:");
            string inchInput2 = Console.ReadLine();

            // Create Inches objects
            Inches inch1 = service.CreateInches(inchInput1);
            Inches inch2 = service.CreateInches(inchInput2);

            if (inch1 == null || inch2 == null)
            {
                Console.WriteLine("Invalid inch input.");
            }
            else
            {
                bool inchResult = service.AreEqual(inch1, inch2);
                Console.WriteLine($"Inches Equal ({inchResult})");
            }
        }
    }
}