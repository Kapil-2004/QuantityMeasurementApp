﻿using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        // Entry point of the application
        static void Main(string[] args)
        {
            // Create service objects for Length and Weight
            QuantityLengthService lengthService = new QuantityLengthService();
            QuantityWeightService weightService = new QuantityWeightService();

            // Main application loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("  Quantity Measurement  ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Length Operations (UC1–UC8)");
                Console.WriteLine("2. Weight Operations (UC9)");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                // Route to selected module
                switch (choice)
                {
                    case "1":
                        ShowLengthMenu(lengthService);
                        break;
                    case "2":
                        ShowWeightMenu(weightService);
                        break;
                    case "3":
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ===================== LENGTH MENU =====================
        // Displays all Length-related operations (UC1–UC8)
        private static void ShowLengthMenu(QuantityLengthService service)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Length Operations - UC8");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Compare Two Lengths");
            Console.WriteLine("2. Convert Length");
            Console.WriteLine("3. Add Two Lengths");
            Console.WriteLine("4. Add Two Lengths (Specify Target Unit)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            // Calls respective length operation
            switch (choice)
            {
                case "1": CompareLengths(service); break;
                case "2": ConvertLength(service); break;
                case "3": AddLengths(service); break;
                case "4": AddLengthsWithTarget(service); break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }

        private static void AddLengthsWithTarget(QuantityLengthService service)
        {
            throw new NotImplementedException();
        }

        private static void AddLengths(QuantityLengthService service)
        {
            throw new NotImplementedException();
        }

        private static void CompareLengths(QuantityLengthService service)
        {
            throw new NotImplementedException();
        }

        private static void ConvertLength(QuantityLengthService service)
        {
            throw new NotImplementedException();
        }

        // ===================== WEIGHT MENU =====================
        // Displays all Weight-related operations (UC9)
        private static void ShowWeightMenu(QuantityWeightService service)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Weight Operations - UC9");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Compare Two Weights");
            Console.WriteLine("2. Convert Weight");
            Console.WriteLine("3. Add Two Weights");
            Console.WriteLine("4. Add Two Weights (Specify Target Unit)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            // Calls respective weight operation
            switch (choice)
            {
                case "1": CompareWeights(service); break;
                case "2": ConvertWeight(service); break;
                case "3": AddWeights(service); break;
                case "4": AddWeightsWithTarget(service); break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }

        // ===================== WEIGHT OPERATIONS =====================

        // Compares two weight quantities for equality
        private static void CompareWeights(QuantityWeightService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                WeightUnit unit1 = ReadWeightUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                WeightUnit unit2 = ReadWeightUnit();

                // Uses service layer for comparison logic
                bool result = service.AreEqual(value1, unit1, value2, unit2);

                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"Result: {(result ? "Equal (True)" : "Not Equal (False)")}");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        // Converts weight from one unit to another
        private static void ConvertWeight(QuantityWeightService service)
        {
            try
            {
                Console.WriteLine("\nEnter Quantity to Convert:");
                double value = ReadDouble("Value: ");
                WeightUnit sourceUnit = ReadWeightUnit();

                Console.WriteLine("\nConvert To:");
                WeightUnit targetUnit = ReadWeightUnit();

                // Uses service layer for conversion
                double result = service.Convert(value, sourceUnit, targetUnit);

                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"{value} {sourceUnit} = {result} {targetUnit}");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        // Adds two weights and returns result in first unit
        private static void AddWeights(QuantityWeightService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                WeightUnit unit1 = ReadWeightUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                WeightUnit unit2 = ReadWeightUnit();

                // Uses service Add method
                QuantityWeight result = service.Add(value1, unit1, value2, unit2);

                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"{value1} {unit1} + {value2} {unit2} = {result.Value} {result.Unit}");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        // Adds two weights and converts result to specified target unit
        private static void AddWeightsWithTarget(QuantityWeightService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                WeightUnit unit1 = ReadWeightUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                WeightUnit unit2 = ReadWeightUnit();

                Console.WriteLine("\nSelect Target Unit:");
                WeightUnit targetUnit = ReadWeightUnit();

                // Uses overloaded Add method with target unit
                QuantityWeight result =
                    service.Add(value1, unit1, value2, unit2, targetUnit);

                Console.WriteLine("\n---------------------------------");
                Console.WriteLine($"{value1} {unit1} + {value2} {unit2} = {result.Value} {result.Unit}");
                Console.WriteLine("---------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Pause();
        }

        // ===================== INPUT HELPERS =====================

        // Reads weight unit from user input
        private static WeightUnit ReadWeightUnit()
        {
            while (true)
            {
                Console.WriteLine("Select Unit:");
                Console.WriteLine("1. Kilogram");
                Console.WriteLine("2. Gram");
                Console.WriteLine("3. Pound");
                Console.Write("Choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return WeightUnit.Kilogram;
                    case "2": return WeightUnit.Gram;
                    case "3": return WeightUnit.Pound;
                    default:
                        Console.WriteLine("Invalid unit selection. Try again.");
                        break;
                }
            }
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

        // Pauses execution until key press
        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}