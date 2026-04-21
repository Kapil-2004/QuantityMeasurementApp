using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Engines;

namespace QuantityMeasurement.CompareService.Services
{
    public interface ICompareService
    {
        bool Compare(QuantityDTO q1, QuantityDTO q2);
    }

    public class CompareServiceImpl : ICompareService
    {
        private readonly ILogger<CompareServiceImpl> _logger;

        public CompareServiceImpl(ILogger<CompareServiceImpl> logger)
        {
            _logger = logger;
        }

        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                // Validate same measurement type
                ValidationEngine.ValidateSameMeasurement(q1.MeasurementType, q2.MeasurementType);

                // Convert both to base unit
                double q1BaseValue = ConversionEngine.ConvertToBase(q1);
                double q2BaseValue = ConversionEngine.ConvertToBase(q2);

                // Compare with small tolerance for floating point
                bool result = Math.Abs(q1BaseValue - q2BaseValue) < 0.0001;
                _logger.LogInformation($"Comparison result: {result}");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Compare operation");
                throw;
            }
        }
    }
}
