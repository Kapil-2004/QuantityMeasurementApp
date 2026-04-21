using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.CompareService.Services;
using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Models.Request;
using QuantityMeasurement.SharedModels.Models.Response;

namespace QuantityMeasurement.CompareService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantitiesController : ControllerBase
    {
        private readonly ICompareService _service;
        private readonly ILogger<QuantitiesController> _logger;

        public QuantitiesController(ICompareService service, ILogger<QuantitiesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("compare")]
        public IActionResult Compare([FromBody] BinaryOperationRequest request, [FromQuery] bool isAuthenticated)
        {
            try
            {
                _logger.LogInformation($"Compare request received - Authenticated: {isAuthenticated}");

                if (request?.Q1 == null || request?.Q2 == null)
                    return BadRequest(new ErrorResponse("Q1 and Q2 are required"));

                var q1Dto = new QuantityDTO(request.Q1.Value, request.Q1.Unit, request.Q1.MeasurementType);
                var q2Dto = new QuantityDTO(request.Q2.Value, request.Q2.Unit, request.Q2.MeasurementType);

                bool result = _service.Compare(q1Dto, q2Dto);

                var response = new ComparisonResponse
                {
                    AreEqual = result,
                    Message = result ? "Quantities are equal" : "Quantities are not equal"
                };

                return Ok(new ApiResponse<ComparisonResponse>(true, "Comparison successful", response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in compare endpoint");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }
    }
}
