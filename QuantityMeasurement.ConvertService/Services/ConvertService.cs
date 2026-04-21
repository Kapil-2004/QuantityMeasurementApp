using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Engines;

namespace QuantityMeasurement.ConvertService.Services
{
    public interface IConvertService
    {
        QuantityDTO Convert(QuantityDTO quantity, string targetUnit);
    }

    public class ConvertServiceImpl : IConvertService
    {
        private readonly ILogger<ConvertServiceImpl> _logger;

        public ConvertServiceImpl(ILogger<ConvertServiceImpl> logger)
        {
            _logger = logger;
        }

        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
        {
            try
            {
                // Validate target unit exists
                ValidateUnit(quantity.MeasurementType, targetUnit);

                // Convert to base unit
                double baseValue = ConversionEngine.ConvertToBase(quantity);

                // Convert from base to target unit
                double convertedValue = ConversionEngine.ConvertFromBase(quantity.MeasurementType, targetUnit, baseValue);

                _logger.LogInformation($"Converted {quantity.Value}{quantity.Unit} to {convertedValue}{targetUnit}");

                return new QuantityDTO(convertedValue, targetUnit, quantity.MeasurementType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Convert operation");
                throw;
            }
        }

        private void ValidateUnit(string measurementType, string unit)
        {
            try
            {
                _ = measurementType switch
                {
                    "Length" => Enum.Parse<QuantityMeasurement.SharedModels.Enums.LengthUnit>(unit),
                    "Weight" => Enum.Parse<QuantityMeasurement.SharedModels.Enums.WeightUnit>(unit),
                    "Volume" => Enum.Parse<QuantityMeasurement.SharedModels.Enums.VolumeUnit>(unit),
                    "Temperature" => Enum.Parse<QuantityMeasurement.SharedModels.Enums.TemperatureUnit>(unit),
                    _ => throw new ArgumentException("Invalid Measurement Type")
                };
            }
            catch
            {
                throw new ArgumentException($"Invalid unit '{unit}' for measurement type '{measurementType}'");
            }
        }
    }
}
