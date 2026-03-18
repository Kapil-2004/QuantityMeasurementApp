using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementConsole.UI;
using QuantityMeasurementConsole.Interfaces;
using QuantityMeasurementModelLayer.Configuration;
using QuantityMeasurementRepositoryLayer.Implementations;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementConsole
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Quantity Measurement System";

            // Setup Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .AddSingleton<ApplicationConfig>()
                // Switch uncomment here to use Cache instead of Database.
                // .AddSingleton<IQuantityMeasurementRepository, QuantityMeasurementCacheRepository>(provider => QuantityMeasurementCacheRepository.GetInstance())
                .AddScoped<IQuantityMeasurementRepository, QuantityMeasurementDatabaseRepository>()
                .AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>()
                .AddScoped<QuantityMeasurementController>()
                .AddScoped<IMenu, Menu>()
                .BuildServiceProvider();

            // Run the application
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Starting application with Database Repository...");

            var menu = serviceProvider.GetRequiredService<IMenu>();
            try
            {
                menu.Start();
            }
            finally
            {
                // Ensure resources like the DB connections are closed cleanly
                var repo = serviceProvider.GetService<IQuantityMeasurementRepository>();
                repo?.CloseResources();

                logger.LogInformation("Shutting down application...");
                if (serviceProvider is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}