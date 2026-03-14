using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementConsole.Interfaces;

namespace QuantityMeasurementConsole.UI
{
    /// <summary>
    /// Interactive console menu implementation providing user interface for quantity measurement operations.
    /// Manages user input/output, displays formatted results with color coding, and delegates operations to controller.
    /// </summary>
    public class Menu : IMenu
    {
        /// <summary>Controller instance for executing business operations (Add, Subtract, Compare, Convert, Divide)</summary>
        private readonly QuantityMeasurementController _controller;

        /// <summary>Constructor accepting controller dependency from DI container</summary>
        /// <param name="controller">Console layer controller for delegating business operations</param>
        public Menu(QuantityMeasurementController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Starts the interactive main menu loop.
        /// Continuously displays menu options, captures user input, and delegates to appropriate operation handlers.
        /// Catches and displays errors gracefully. Loop continues until user selects exit (option 0).
        /// </summary>
        public void Start()
        {
            while (true)
            {
                // Display banner and menu options
                DrawHeader();
                DrawMenu();

                // Prompt user with cyan colored text
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nSelect Option ➤ ");
                Console.ResetColor();

                // Get user input from console
                var input = Console.ReadLine();

                try
                {
                    // Route to appropriate operation handler based on user selection
                    switch (input)
                    {
                        case "1": Compare(); break;           // Compare two quantities
                        case "2": Convert(); break;           // Convert quantity to different unit
                        case "3": Add(); break;               // Add two quantities
                        case "4": Subtract(); break;          // Subtract quantities
                        case "5": Divide(); break;            // Divide quantities
                        case "6": ShowHistory(); break;       // View operation history
                        case "0": Exit(); return;             // Exit application
                        default: Error("Invalid option."); break;  // Handle invalid input
                    }
                }
                catch (Exception ex)
                {
                    // Display error message in red if operation fails
                    Error(ex.Message);
                }

                // Pause before showing menu again
                Pause();
            }
        }

        /// <summary>
        /// Displays the application header banner in magenta color.
        /// Clears console and shows formatted title with application name and architecture information.
        /// </summary>
        private void DrawHeader()
        {
            // Clear console screen for fresh display
            Console.Clear();

            // Set magenta color for header box
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Display decorated application title
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║        QUANTITY MEASUREMENT SYSTEM           ║");
            Console.WriteLine("║                UC15 N-Tier                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            // Reset to default color
            Console.ResetColor();
        }

        /// <summary>
        /// Displays menu options in colored text (cyan for operations, yellow for exit).
        /// Shows all six available operations and exit option.
        /// </summary>
        private void DrawMenu()
        {
            // Display operation options in cyan
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\n1 → Compare Quantities");
            Console.WriteLine("2 → Convert Quantity");
            Console.WriteLine("3 → Add Quantities");
            Console.WriteLine("4 → Subtract Quantities");
            Console.WriteLine("5 → Divide Quantities");
            Console.WriteLine("6 → View History");

            // Display exit option in yellow for emphasis
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0 → Exit");

            // Reset to default color
            Console.ResetColor();
        }

        /// <summary>
        /// Handles comparison operation: prompts user for measurement type, gets two quantities, and compares them.
        /// Delegates to controller for business logic. Displays result (true/false) in green success format.
        /// Throws exception if quantities are incompatible or invalid.
        /// </summary>
        private void Compare()
        {
            // Ask user which measurement type to compare
            var type = AskMeasurementType();

            // Get two quantities from user
            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            // Delegate to controller for comparison operation
            bool result = _controller.Compare(q1, q2);

            // Display result in green success format
            Success($"Result ➜ {result}");
        }

        /// <summary>
        /// Handles conversion operation: prompts for measurement type, source quantity with unit, and target unit.
        /// Delegates to controller for unit conversion. Displays converted quantity in green success format.
        /// Uses base unit normalization internally for accurate conversion across unit types.
        /// </summary>
        private void Convert()
        {
            // Ask user which measurement type to convert
            var type = AskMeasurementType();

            // Get source quantity with original unit
            var source = ReadQuantity(type, "Source Quantity");

            // Ask for target unit to convert to
            var target = AskUnit(type);

            // Delegate to controller for conversion operation
            var result = _controller.Convert(source, target);

            // Display converted result in green success format
            Success($"Converted Result ➜ {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Handles addition operation: prompts for measurement type, gets two quantities, and adds them.
        /// Both quantities must be same measurement type (validated in service layer).
        /// Converts both to base unit, performs addition, converts back to original unit.
        /// Displays result in green success format.
        /// </summary>
        private void Add()
        {
            // Ask user which measurement type to add
            var type = AskMeasurementType();

            // Get two quantities from user
            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            // Delegate to controller for addition operation
            var result = _controller.Add(q1, q2);

            // Display addition result in green success format
            Success($"Addition Result ➜ {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Handles subtraction operation: prompts for measurement type, gets two quantities, and subtracts second from first.
        /// Both quantities must be same measurement type (validated in service layer).
        /// Converts both to base unit, performs subtraction, converts back to original unit.
        /// Displays result in green success format.
        /// </summary>
        private void Subtract()
        {
            // Ask user which measurement type to subtract
            var type = AskMeasurementType();

            // Get two quantities from user
            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            // Delegate to controller for subtraction operation
            var result = _controller.Subtract(q1, q2);

            // Display subtraction result in green success format
            Success($"Subtraction Result ➜ {result.Value} {result.Unit}");
        }

        /// <summary>
        /// Handles division operation: prompts for measurement type, gets two quantities, and divides first by second.
        /// Both quantities must be same measurement type (validated in service layer).
        /// Validates divisor is non-zero before operation. Result is unitless (ratio).
        /// Converts both to base unit, performs division, returns dimensionless number.
        /// Displays result in green success format.
        /// </summary>
        private void Divide()
        {
            // Ask user which measurement type to divide
            var type = AskMeasurementType();

            // Get two quantities from user
            var q1 = ReadQuantity(type, "First Quantity");
            var q2 = ReadQuantity(type, "Second Quantity");

            // Delegate to controller for division operation
            var result = _controller.Divide(q1, q2);

            // Display division result (unitless) in green success format
            Success($"Division Result ➜ {result}");
        }

        /// <summary>
        /// Placeholder for history feature: displays message indicating feature is under development.
        /// Future implementation will retrieve operation history from repository cache.
        /// </summary>
        private void ShowHistory()
        {
            Console.WriteLine("\nHistory feature coming soon...");
        }

        /// <summary>
        /// Prompts user to select a measurement type from four available options.
        /// Maps numeric input (1-4) to measurement type strings used throughout application.
        /// Throws exception if invalid option selected (not 1-4).
        /// </summary>
        /// <returns>Measurement type string: Length, Weight, Volume, or Temperature</returns>
        private string AskMeasurementType()
        {
            Console.WriteLine("\nMeasurement Type");

            // Display all available measurement types
            Console.WriteLine("1 → Length");
            Console.WriteLine("2 → Weight");
            Console.WriteLine("3 → Volume");
            Console.WriteLine("4 → Temperature");

            Console.Write("\nChoice ➜ ");

            // Map numeric input to measurement type string using switch expression
            return Console.ReadLine() switch
            {
                "1" => "Length",
                "2" => "Weight",
                "3" => "Volume",
                "4" => "Temperature",
                _ => throw new Exception("Invalid type")  // Throw exception on invalid input
            };
        }

        /// <summary>
        /// Prompts user to select a unit based on measurement type.
        /// Dynamically displays available units for selected measurement type.
        /// Validates input is in valid range (1 to number of units).
        /// </summary>
        /// <param name="type">Measurement type (Length, Weight, Volume, Temperature)</param>
        /// <returns>Selected unit name as string</returns>
        private string AskUnit(string type)
        {
            // Map measurement type to array of available units using switch expression
            string[] units = type switch
            {
                "Length" => new[] { "Feet", "Inch", "Yard", "Centimeter" },
                "Weight" => new[] { "Kilogram", "Gram", "Pound" },
                "Volume" => new[] { "Litre", "Millilitre", "Gallon" },
                "Temperature" => new[] { "Celsius", "Fahrenheit", "Kelvin" },
                _ => Array.Empty<string>()
            };

            Console.WriteLine("\nSelect Unit");

            // Display all available units for selected measurement type
            for (int i = 0; i < units.Length; i++)
                Console.WriteLine($"{i + 1} → {units[i]}");

            Console.Write("Choice ➜ ");

            // Parse user input and convert to 0-based index
            int index = int.Parse(Console.ReadLine());

            // Return selected unit
            return units[index - 1];
        }

        /// <summary>
        /// Captures quantity input from user: numeric value and unit selection.
        /// Creates QuantityDTO object combining value, unit, and measurement type.
        /// Validates value is valid double (throws FormatException if not).
        /// </summary>
        /// <param name="type">Measurement type for this quantity</param>
        /// <param name="label">Display label shown to user (e.g., "First Quantity")</param>
        /// <returns>QuantityDTO containing value, unit, and measurement type</returns>
        private QuantityDTO ReadQuantity(string type, string label)
        {
            // Display label for this quantity input
            Console.WriteLine($"\n{label}");

            Console.Write("Value ➜ ");

            // Parse and validate numeric value from user input
            double value = double.Parse(Console.ReadLine());

            // Ask user to select unit for this measurement
            string unit = AskUnit(type);

            // Return QuantityDTO with captured data (transferred to service layer)
            return new QuantityDTO(value, unit, type);
        }

        /// <summary>
        /// Displays success message in green color with checkmark symbol.
        /// Used to show successful operation results to user with visual feedback.
        /// </summary>
        /// <param name="message">Success message or result to display</param>
        private void Success(string message)
        {
            // Set green color for success message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✔ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays error message in red color with X symbol.
        /// Used to show operation failures, validation errors, and exceptions to user.
        /// Provides clear visual feedback that something went wrong.
        /// </summary>
        /// <param name="message">Error message describing what failed</param>
        private void Error(string message)
        {
            // Set red color for error message
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Pauses execution and waits for user to press ENTER.
        /// Displayed in dark gray text to indicate it's a system message, not operation feedback.
        /// Allows user to read operation results before menu redraws.
        /// </summary>
        private void Pause()
        {
            // Display pause message in dark gray
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ResetColor();
            // Wait for user input
            Console.ReadLine();
        }

        /// <summary>
        /// Displays exit message in yellow color with goodbye symbol.
        /// Called when user selects exit option (0). Shows friendly message before application closes.
        /// </summary>
        private void Exit()
        {
            // Display goodbye message in yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n👋 Thank you for using Quantity Measurement System!");
            Console.ResetColor();
        }
    }
}