using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuantityLengthService service = new QuantityLengthService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("  Quantity Measurement - UC3");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Compare Two Lengths");
                Console.WriteLine("2. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CompareLengths(service);
                        break;

                    case "2":
                        Console.WriteLine("Exiting application...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Handles user input and comparison logic
        private static void CompareLengths(QuantityLengthService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                LengthUnit unit1 = ReadUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                LengthUnit unit2 = ReadUnit();

                bool result = service.AreEqual(value1, unit1, value2, unit2);

                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"Result: {(result ? "Equal (True)" : "Not Equal (False)")}");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Reads and validates numeric input
        private static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;

                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        // Reads and validates unit input
        private static LengthUnit ReadUnit()
        {
            while (true)
            {
                Console.WriteLine("Select Unit:");
                Console.WriteLine("1. Feet");
                Console.WriteLine("2. Inch");
                Console.WriteLine("3. Yard");
                Console.WriteLine("4. Centimeter");
                Console.Write("Choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return LengthUnit.Feet;
                    case "2": return LengthUnit.Inch;
                    case "3": return LengthUnit.Yard;
                    case "4": return LengthUnit.Centimeter;
                    default:
                        Console.WriteLine("Invalid unit selection. Try again.");
                        break;
                }
            }
        }
    }
}