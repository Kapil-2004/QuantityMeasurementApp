using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementConsole.Interfaces;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Models;

namespace QuantityMeasurementConsole.UI
{
    public class Menu : IMenu
    {
        private readonly QuantityMeasurementController _controller;

        public Menu(QuantityMeasurementController controller)
        {
            _controller = controller;
        }

        public void Start()
        {
            while (true)
            {
                DrawHeader();
                DrawMenu();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nSelect Option ➤ ");
                Console.ResetColor();

                var input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1": Compare(); break;
                        case "2": Convert(); break;
                        case "3": Add(); break;
                        case "4": Subtract(); break;
                        case "5": Divide(); break;
                        case "6": ShowHistory(); break;
                        case "0": Exit(); return;
                        default: Error("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Error(ex.Message);
                }

                Pause();
            }
        }

        private void DrawHeader()
        {
            try { Console.Clear(); } catch (System.IO.IOException) { /* Ignore non-interactive terminals */ }

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║        QUANTITY MEASUREMENT SYSTEM           ║");
            Console.WriteLine("║                UC15 N-Tier                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.ResetColor();
        }

        private void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\n1 → Compare Quantities");
            Console.WriteLine("2 → Convert Quantity");
            Console.WriteLine("3 → Add Quantities");
            Console.WriteLine("4 → Subtract Quantities");
            Console.WriteLine("5 → Divide Quantities");
            Console.WriteLine("6 → View History");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0 → Exit");

            Console.ResetColor();
        }

        private void Compare()
        {
            var type = AskMeasurementType();

            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            bool result = _controller.Compare(q1, q2);

            Success($"Result ➜ {result}");
        }

        private void Convert()
        {
            var type = AskMeasurementType();

            var source = ReadQuantity(type, "Source Quantity");

            var target = AskUnit(type);

            var result = _controller.Convert(source, target);

            Success($"Converted Result ➜ {result.Value} {result.Unit}");
        }

        private void Add()
        {
            var type = AskMeasurementType();

            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            var result = _controller.Add(q1, q2);

            Success($"Addition Result ➜ {result.Value} {result.Unit}");
        }

        private void Subtract()
        {
            var type = AskMeasurementType();

            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            var result = _controller.Subtract(q1, q2);

            Success($"Subtraction Result ➜ {result.Value} {result.Unit}");
        }

        private void Divide()
        {
            var type = AskMeasurementType();

            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            var result = _controller.Divide(q1, q2);

            Success($"Division Result ➜ {result}");
        }

        private void ShowHistory()
        {
            var history = _controller.GetHistory();
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       OPERATION HISTORY                                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();

            if (history.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n⚠ No history found.");
                Console.ResetColor();
            }
            else
            {
                // Display entries in reverse order (newest first)
                for (int i = history.Count; i >= 1; i--)
                {
                    var item = history[i - 1];
                    DisplayHistoryEntry(i, item);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress ENTER to return to menu...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void DisplayHistoryEntry(int id, QuantityMeasurementEntity item)
        {
            string status = item.HasError ? "FAILED" : "SUCCESS";
            ConsoleColor statusColor = item.HasError ? ConsoleColor.Red : ConsoleColor.Green;

            // Get operation type details
            string operationType = item.Operation.ToString();
            string operationDetails = GetOperationTypeWithCategory(item);

            // Format input details
            string inputDetails = FormatInputDetails(item);

            // Format result
            string resultStr = item.HasError 
                ? $"ERROR: {item.ErrorMessage}" 
                : (item.Result?.ToString() ?? "N/A");

            // Format timestamp
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            // Print separator
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n────────────────────────────────────────────────────────────────────────────────");
            Console.ResetColor();

            // Print ID and Type
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"ID: {id}");
            Console.ResetColor();
            Console.Write($"  |  ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Type: {operationDetails}");
            Console.ResetColor();

            // Print Input
            Console.Write("Input: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(inputDetails);
            Console.ResetColor();

            // Print Result
            Console.Write("Result: ");
            Console.ForegroundColor = statusColor;
            Console.WriteLine(resultStr);
            Console.ResetColor();

            // Print Status and Time
            Console.Write("Status: ");
            Console.ForegroundColor = statusColor;
            Console.Write(status);
            Console.ResetColor();
            Console.Write($"  |  ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Time: {timestamp}");
            Console.ResetColor();
        }

        private string GetOperationTypeWithCategory(QuantityMeasurementEntity item)
        {
            string category = GetCategoryFromOperand(item.Operand1);
            return $"{item.Operation} ({category})";
        }

        private string GetCategoryFromOperand(QuantityModel<object>? operand)
        {
            if (operand == null) return "unknown";

            return operand.Unit.GetType().Name switch
            {
                "LengthUnit" => "length",
                "WeightUnit" => "weight",
                "VolumeUnit" => "volume",
                "TemperatureUnit" => "temperature",
                _ => "unknown"
            };
        }

        private string FormatInputDetails(QuantityMeasurementEntity item)
        {
            if (item.Operand1 == null)
                return "N/A";

            string operand1Str = FormatOperand(item.Operand1);

            if (item.Operand2 == null)
                return operand1Str;

            string operand2Str = FormatOperand(item.Operand2);
            string operatorSymbol = item.Operation switch
            {
                OperationType.Convert => " → ",
                OperationType.Add => " + ",
                OperationType.Subtract => " - ",
                OperationType.Divide => " ÷ ",
                OperationType.Compare => " vs ",
                _ => " "
            };

            return $"{operand1Str}{operatorSymbol}{operand2Str}";
        }

        private string FormatOperand(QuantityModel<object> operand)
        {
            return $"{operand.Value} {operand.Unit}";
        }

        private string AskMeasurementType()
        {
            Console.WriteLine("\nMeasurement Type");

            Console.WriteLine("1 → Length");
            Console.WriteLine("2 → Weight");
            Console.WriteLine("3 → Volume");
            Console.WriteLine("4 → Temperature");

            Console.Write("\nChoice ➜ ");

            return Console.ReadLine() switch
            {
                "1" => "Length",
                "2" => "Weight",
                "3" => "Volume",
                "4" => "Temperature",
                _ => throw new Exception("Invalid type")
            };
        }

        private string AskUnit(string type)
        {
            string[] units = type switch
            {
                "Length" => new[] { "Feet", "Inch", "Yard", "Centimeter" },
                "Weight" => new[] { "Kilogram", "Gram", "Pound" },
                "Volume" => new[] { "Litre", "Millilitre", "Gallon" },
                "Temperature" => new[] { "Celsius", "Fahrenheit", "Kelvin" },
                _ => Array.Empty<string>()
            };

            Console.WriteLine("\nSelect Unit");

            for (int i = 0; i < units.Length; i++)
                Console.WriteLine($"{i + 1} → {units[i]}");

            Console.Write("Choice ➜ ");

            int index = int.Parse(Console.ReadLine());

            return units[index - 1];
        }

        private QuantityDTO ReadQuantity(string type, string label)
        {
            Console.WriteLine($"\n{label}");

            Console.Write("Value ➜ ");

            double value = double.Parse(Console.ReadLine());

            string unit = AskUnit(type);

            return new QuantityDTO(value, unit, type);
        }

        private void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✔ {message}");
            Console.ResetColor();
        }

        private void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {message}");
            Console.ResetColor();
        }

        private void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n👋 Thank you for using Quantity Measurement System!");
            Console.ResetColor();
        }
    }
}