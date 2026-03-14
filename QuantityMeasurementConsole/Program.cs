using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementConsole.UI;
using QuantityMeasurementConsole.Interfaces;
using QuantityMeasurementRepositoryLayer.Implementations;

namespace QuantityMeasurementConsole
{
    /// <summary>
    /// Application entry point for the Quantity Measurement System.
    /// Initializes the N-Tier architecture by creating and injecting dependencies.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main entry point of the application.
        /// Sets up the application title and initializes all layers following the dependency injection pattern.
        /// Flow: Repository → Service → Controller → Menu
        /// </summary>
        static void Main()
        {
            // Set console window title
            Console.Title = "Quantity Measurement System";

            // Layer 4: Create repository (persistence layer)
            var repository = QuantityMeasurementCacheRepository.GetInstance();

            // Layer 3: Create service and inject repository
            var service = new QuantityMeasurementServiceImpl(repository);

            // Layer 2: Create controller and inject service
            var controller = new QuantityMeasurementController(service);

            // Layer 1: Create menu and inject controller
            IMenu menu = new Menu(controller);

            // Start the interactive menu
            menu.Start();
        }
    }
}