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
            // Create service objects for Length, Weight and Volume
            QuantityLengthService lengthService = new QuantityLengthService();
            QuantityWeightService weightService = new QuantityWeightService();
            QuantityVolumeService volumeService = new QuantityVolumeService();

            // Main application loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("  Quantity Measurement  (UC11)");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Length Operations (UC1–UC8)");
                Console.WriteLine("2. Weight Operations (UC9)");
                Console.WriteLine("3. Volume Operations (UC11)");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowLengthMenu(lengthService);
                        break;
                    case "2":
                        ShowWeightMenu(weightService);
                        break;
                    case "3":
                        ShowVolumeMenu(volumeService);
                        break;
                    case "4":
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
        private static void ShowLengthMenu(QuantityLengthService service)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Length Operations - UC10");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Compare Two Lengths");
            Console.WriteLine("2. Convert Length");
            Console.WriteLine("3. Add Two Lengths");
            Console.WriteLine("4. Add Two Lengths (Specify Target Unit)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

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

        // ===================== LENGTH OPERATIONS =====================

        private static void CompareLengths(QuantityLengthService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                LengthUnit unit1 = ReadLengthUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                LengthUnit unit2 = ReadLengthUnit();

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

        private static void ConvertLength(QuantityLengthService service)
        {
            try
            {
                Console.WriteLine("\nEnter Quantity to Convert:");
                double value = ReadDouble("Value: ");
                LengthUnit sourceUnit = ReadLengthUnit();

                Console.WriteLine("\nConvert To:");
                LengthUnit targetUnit = ReadLengthUnit();

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

        private static void AddLengths(QuantityLengthService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                LengthUnit unit1 = ReadLengthUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                LengthUnit unit2 = ReadLengthUnit();

                Quantity<LengthUnit> result =
                    service.Add(value1, unit1, value2, unit2);

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

        private static void AddLengthsWithTarget(QuantityLengthService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                LengthUnit unit1 = ReadLengthUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                LengthUnit unit2 = ReadLengthUnit();

                Console.WriteLine("\nSelect Target Unit:");
                LengthUnit targetUnit = ReadLengthUnit();

                Quantity<LengthUnit> result =
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

        // ===================== WEIGHT MENU =====================
        private static void ShowWeightMenu(QuantityWeightService service)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Weight Operations - UC10");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Compare Two Weights");
            Console.WriteLine("2. Convert Weight");
            Console.WriteLine("3. Add Two Weights");
            Console.WriteLine("4. Add Two Weights (Specify Target Unit)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

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

        private static void ConvertWeight(QuantityWeightService service)
        {
            try
            {
                Console.WriteLine("\nEnter Quantity to Convert:");
                double value = ReadDouble("Value: ");
                WeightUnit sourceUnit = ReadWeightUnit();

                Console.WriteLine("\nConvert To:");
                WeightUnit targetUnit = ReadWeightUnit();

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

                Quantity<WeightUnit> result =
                    service.Add(value1, unit1, value2, unit2);

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

                Quantity<WeightUnit> result =
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

        // ===================== VOLUME MENU =====================
        private static void ShowVolumeMenu(QuantityVolumeService service)
        {
            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("  Volume Operations - UC11");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Compare Two Volumes");
            Console.WriteLine("2. Convert Volume");
            Console.WriteLine("3. Add Two Volumes");
            Console.WriteLine("4. Add Two Volumes (Specify Target Unit)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CompareVolumes(service); break;
                case "2": ConvertVolume(service); break;
                case "3": AddVolumes(service); break;
                case "4": AddVolumesWithTarget(service); break;
                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }

        // ===================== VOLUME OPERATIONS =====================

        private static void CompareVolumes(QuantityVolumeService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                VolumeUnit unit1 = ReadVolumeUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                VolumeUnit unit2 = ReadVolumeUnit();

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

        private static void ConvertVolume(QuantityVolumeService service)
        {
            try
            {
                Console.WriteLine("\nEnter Quantity to Convert:");
                double value = ReadDouble("Value: ");
                VolumeUnit sourceUnit = ReadVolumeUnit();

                Console.WriteLine("\nConvert To:");
                VolumeUnit targetUnit = ReadVolumeUnit();

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

        private static void AddVolumes(QuantityVolumeService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                VolumeUnit unit1 = ReadVolumeUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                VolumeUnit unit2 = ReadVolumeUnit();

                Quantity<VolumeUnit> result =
                    service.Add(value1, unit1, value2, unit2);

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

        private static void AddVolumesWithTarget(QuantityVolumeService service)
        {
            try
            {
                Console.WriteLine("\nEnter First Quantity:");
                double value1 = ReadDouble("Value: ");
                VolumeUnit unit1 = ReadVolumeUnit();

                Console.WriteLine("\nEnter Second Quantity:");
                double value2 = ReadDouble("Value: ");
                VolumeUnit unit2 = ReadVolumeUnit();

                Console.WriteLine("\nSelect Target Unit:");
                VolumeUnit targetUnit = ReadVolumeUnit();

                Quantity<VolumeUnit> result =
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

        private static LengthUnit ReadLengthUnit()
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

        private static VolumeUnit ReadVolumeUnit()
        {
            while (true)
            {
                Console.WriteLine("Select Unit:");
                Console.WriteLine("1. Liter");
                Console.WriteLine("2. Milliliter");
                Console.WriteLine("3. Gallon");
                Console.Write("Choice: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return VolumeUnit.Litre;
                    case "2": return VolumeUnit.Millilitre;
                    case "3": return VolumeUnit.Gallon;
                    default:
                        Console.WriteLine("Invalid unit selection. Try again.");
                        break;
                }
            }
        }

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

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}