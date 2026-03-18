using Microsoft.Extensions.Configuration;
using System.IO;

namespace QuantityMeasurementModelLayer.Configuration
{
    public class ApplicationConfig
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public string GetConnectionString(string name = "QuantityMeasurementDB")
        {
            return _configuration.GetConnectionString(name);
        }
    }
}
