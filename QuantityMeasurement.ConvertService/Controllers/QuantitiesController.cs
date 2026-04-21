using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.ConvertService.Services;
using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Models.Request;
using QuantityMeasurement.SharedModels.Models.Response;

namespace QuantityMeasurement.ConvertService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantitiesController : ControllerBase
    {
        private readonly IConvertService _service;
        private readonly ILogger<QuantitiesController> _logger;

        public QuantitiesController(IConvertService service, ILogger<QuantitiesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("convert")]
        public IActionResult Convert([FromBody] ConversionRequest request, [FromQuery] bool isAuthenticated)
        {
            try
            {
                _logger.LogInformation($"Convert request received - Authenticated: {isAuthenticated}");

                if (request?.Quantity == null || string.IsNullOrEmpty(request?.TargetUnit))
                    return BadRequest(new ErrorResponse("Quantity and TargetUnit are required"));

                var quantityDto = new QuantityDTO(request.Quantity.Value, request.Quantity.Unit, request.Quantity.MeasurementType);
                var convertedQuantity = _service.Convert(quantityDto, request.TargetUnit);

                var response = new ConversionResponse
                {
                    Result = convertedQuantity.Value,
                    Unit = convertedQuantity.Unit,
                    MeasurementType = convertedQuantity.MeasurementType
                };

                return Ok(new ApiResponse<ConversionResponse>(true, "Conversion successful", response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in convert endpoint");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }
    }
}
