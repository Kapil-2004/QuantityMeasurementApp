﻿using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
    static void Main(string[] args)
    {
    QuantityLengthService lengthService = new QuantityLengthService();
    QuantityWeightService weightService = new QuantityWeightService();
    QuantityVolumeService volumeService = new QuantityVolumeService();
    QuantityTemperatureService temperatureService = new QuantityTemperatureService(); // UC14

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("   Quantity Measurement System");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Volume Operations");
                Console.WriteLine("4. Temperature Operations");
                Console.WriteLine("5. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine() ?? "";

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
                        ShowTemperatureMenu(temperatureService);
                        break;
                    case "5":
                        return;
                }
            }
        }

        // ================= LENGTH MENU =================

        private static void ShowLengthMenu(QuantityLengthService service)
        {
            Console.Clear();
            Console.WriteLine("==== Length Operations ====");
            Console.WriteLine("1. Compare Lengths");
            Console.WriteLine("2. Convert Length");
            Console.WriteLine("3. Add Lengths");
            Console.WriteLine("4. Add Lengths (Target Unit)");
            Console.WriteLine("5. Subtract Lengths");
            Console.WriteLine("6. Divide Lengths");
            Console.WriteLine();
            Console.Write("Select option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": CompareLengths(service); break;
                case "2": ConvertLength(service); break;
                case "3": AddLengths(service); break;
                case "4": AddLengthsWithTarget(service); break;
                case "5": SubtractLengths(); break;
                case "6": DivideLengths(); break;
            }
        }

        // ================= WEIGHT MENU =================

        private static void ShowWeightMenu(QuantityWeightService service)
        {
            Console.Clear();
            Console.WriteLine("==== Weight Operations ====");
            Console.WriteLine("1. Compare Weights");
            Console.WriteLine("2. Convert Weight");
            Console.WriteLine("3. Add Weights");
            Console.WriteLine("4. Add Weights (Target Unit)");
            Console.WriteLine("5. Subtract Weights");
            Console.WriteLine("6. Divide Weights");
            Console.WriteLine();
            Console.Write("Select option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": CompareWeights(service); break;
                case "2": ConvertWeight(service); break;
                case "3": AddWeights(service); break;
                case "4": AddWeightsWithTarget(service); break;
                case "5": SubtractWeights(); break;
                case "6": DivideWeights(); break;
            }
        }

        // ================= VOLUME MENU =================

        private static void ShowVolumeMenu(QuantityVolumeService service)
        {
            Console.Clear();
            Console.WriteLine("==== Volume Operations ====");
            Console.WriteLine("1. Compare Volumes");
            Console.WriteLine("2. Convert Volume");
            Console.WriteLine("3. Add Volumes");
            Console.WriteLine("4. Add Volumes (Target Unit)");
            Console.WriteLine("5. Subtract Volumes");
            Console.WriteLine("6. Divide Volumes");
            Console.WriteLine();
            Console.Write("Select option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": CompareVolumes(service); break;
                case "2": ConvertVolume(service); break;
                case "3": AddVolumes(service); break;
                case "4": AddVolumesWithTarget(service); break;
                case "5": SubtractVolumes(); break;
                case "6": DivideVolumes(); break;
            }
        }

        // ================= TEMPERATURE MENU =================

        private static void ShowTemperatureMenu(QuantityTemperatureService service)
        {
            Console.Clear();
            Console.WriteLine("==== Temperature Operations ====");
            Console.WriteLine("1. Compare Temperatures");
            Console.WriteLine("2. Convert Temperature");
            Console.WriteLine();
            Console.Write("Select option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1": CompareTemperatures(service); break;
                case "2": ConvertTemperature(service); break;
            }
        }

        // ================= LENGTH OPERATIONS =================

        private static void CompareLengths(QuantityLengthService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadDouble("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            bool result = service.AreEqual(v1, u1, v2, u2);

            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private static void ConvertLength(QuantityLengthService service)
        {
            double v = ReadDouble("Enter value: ");
            LengthUnit s = ReadLengthUnit();
            LengthUnit t = ReadLengthUnit();

            double result = service.Convert(v, s, t);

            Console.WriteLine($"{v} {s} = {result} {t}");
            Pause();
        }

        private static void AddLengths(QuantityLengthService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadDouble("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            var result = service.Add(v1, u1, v2, u2);

            Console.WriteLine(result);
            Pause();
        }

        private static void AddLengthsWithTarget(QuantityLengthService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadDouble("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            LengthUnit target = ReadLengthUnit();

            var result = service.Add(v1, u1, v2, u2, target);

            Console.WriteLine(result);
            Pause();
        }

        private static void SubtractLengths()
        {
            double v1 = ReadDouble("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadDouble("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(v1, u1);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(v2, u2);

            Console.WriteLine(q1.Subtract(q2));
            Pause();
        }

        private static void DivideLengths()
        {
            double v1 = ReadDouble("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadDouble("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            Quantity<LengthUnit> q1 = new Quantity<LengthUnit>(v1, u1);
            Quantity<LengthUnit> q2 = new Quantity<LengthUnit>(v2, u2);

            Console.WriteLine(q1.Divide(q2));
            Pause();
        }

        // ================= WEIGHT OPERATIONS =================

        private static void CompareWeights(QuantityWeightService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadDouble("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            Console.WriteLine(service.AreEqual(v1, u1, v2, u2));
            Pause();
        }

        private static void ConvertWeight(QuantityWeightService service)
        {
            double v = ReadDouble("Enter value: ");
            WeightUnit s = ReadWeightUnit();
            WeightUnit t = ReadWeightUnit();

            Console.WriteLine(service.Convert(v, s, t));
            Pause();
        }

        private static void AddWeights(QuantityWeightService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadDouble("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            Console.WriteLine(service.Add(v1, u1, v2, u2));
            Pause();
        }

        private static void AddWeightsWithTarget(QuantityWeightService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadDouble("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            WeightUnit t = ReadWeightUnit();

            Console.WriteLine(service.Add(v1, u1, v2, u2, t));
            Pause();
        }

        private static void SubtractWeights()
        {
            double v1 = ReadDouble("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadDouble("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            Quantity<WeightUnit> q1 = new Quantity<WeightUnit>(v1, u1);
            Quantity<WeightUnit> q2 = new Quantity<WeightUnit>(v2, u2);

            Console.WriteLine(q1.Subtract(q2));
            Pause();
        }

        private static void DivideWeights()
        {
            double v1 = ReadDouble("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadDouble("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            Quantity<WeightUnit> q1 = new Quantity<WeightUnit>(v1, u1);
            Quantity<WeightUnit> q2 = new Quantity<WeightUnit>(v2, u2);

            Console.WriteLine(q1.Divide(q2));
            Pause();
        }

        // ================= VOLUME OPERATIONS =================

        private static void CompareVolumes(QuantityVolumeService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();

            double v2 = ReadDouble("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            Console.WriteLine(service.AreEqual(v1, u1, v2, u2));
            Pause();
        }

        private static void ConvertVolume(QuantityVolumeService service)
        {
            double v = ReadDouble("Enter value: ");
            VolumeUnit s = ReadVolumeUnit();
            VolumeUnit t = ReadVolumeUnit();

            Console.WriteLine(service.Convert(v, s, t));
            Pause();
        }

        private static void AddVolumes(QuantityVolumeService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();

            double v2 = ReadDouble("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            Console.WriteLine(service.Add(v1, u1, v2, u2));
            Pause();
        }

        private static void AddVolumesWithTarget(QuantityVolumeService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();

            double v2 = ReadDouble("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            VolumeUnit t = ReadVolumeUnit();

            Console.WriteLine(service.Add(v1, u1, v2, u2, t));
            Pause();
        }

        private static void SubtractVolumes()
        {
            double v1 = ReadDouble("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();

            double v2 = ReadDouble("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(v1, u1);
            Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(v2, u2);

            Console.WriteLine(q1.Subtract(q2));
            Pause();
        }

        private static void DivideVolumes()
        {
            double v1 = ReadDouble("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();

            double v2 = ReadDouble("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            Quantity<VolumeUnit> q1 = new Quantity<VolumeUnit>(v1, u1);
            Quantity<VolumeUnit> q2 = new Quantity<VolumeUnit>(v2, u2);

            Console.WriteLine(q1.Divide(q2));
            Pause();
        }

        // ================= TEMPERATURE OPERATIONS =================

        private static void CompareTemperatures(QuantityTemperatureService service)
        {
            double v1 = ReadDouble("Enter first value: ");
            TemperatureUnit u1 = ReadTemperatureUnit();

            double v2 = ReadDouble("Enter second value: ");
            TemperatureUnit u2 = ReadTemperatureUnit();

            Console.WriteLine(service.AreEqual(v1, u1, v2, u2));
            Pause();
        }

        private static void ConvertTemperature(QuantityTemperatureService service)
        {
            double v = ReadDouble("Enter value: ");
            TemperatureUnit s = ReadTemperatureUnit();
            TemperatureUnit t = ReadTemperatureUnit();

            Console.WriteLine(service.Convert(v, s, t));
            Pause();
        }

        // ================= INPUT HELPERS =================

        private static LengthUnit ReadLengthUnit()
        {
            Console.WriteLine("Select Length Unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inch");
            Console.WriteLine("3. Centimeter");
            Console.WriteLine("4. Yard");
            return (LengthUnit)(int.Parse(Console.ReadLine() ?? "") - 1);
        }

        private static WeightUnit ReadWeightUnit()
        {
            Console.WriteLine("Select Weight Unit:");
            Console.WriteLine("1. Kilogram");
            Console.WriteLine("2. Gram");
            Console.WriteLine("3. Pound");
            return (WeightUnit)(int.Parse(Console.ReadLine() ?? "") - 1);
        }

        private static VolumeUnit ReadVolumeUnit()
        {
            Console.WriteLine("Select Volume Unit:");
            Console.WriteLine("1. Litre");
            Console.WriteLine("2. Millilitre");
            Console.WriteLine("3. Gallon");
            return (VolumeUnit)(int.Parse(Console.ReadLine() ?? "") - 1);
        }

        private static TemperatureUnit ReadTemperatureUnit()
        {
            Console.WriteLine("Select Temperature Unit:");
            Console.WriteLine("1. Celsius");
            Console.WriteLine("2. Fahrenheit");
            Console.WriteLine("3. Kelvin");
            return (TemperatureUnit)(int.Parse(Console.ReadLine() ?? "") - 1);
        }

        private static double ReadDouble(string message)
        {
            Console.Write(message);
            return double.Parse(Console.ReadLine() ?? "");
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

}
