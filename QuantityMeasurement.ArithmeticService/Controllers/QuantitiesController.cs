using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.ArithmeticService.Services;
using QuantityMeasurement.SharedModels.DTO;
using QuantityMeasurement.SharedModels.Models.Request;
using QuantityMeasurement.SharedModels.Models.Response;

namespace QuantityMeasurement.ArithmeticService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantitiesController : ControllerBase
    {
        private readonly IArithmeticService _service;
        private readonly ILogger<QuantitiesController> _logger;

        public QuantitiesController(IArithmeticService service, ILogger<QuantitiesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] BinaryOperationRequest request, [FromQuery] bool isAuthenticated)
        {
            try
            {
                _logger.LogInformation($"Add request received - Authenticated: {isAuthenticated}");

                if (request?.Q1 == null || request?.Q2 == null)
                    return BadRequest(new ErrorResponse("Q1 and Q2 are required"));

                var q1Dto = new QuantityDTO(request.Q1.Value, request.Q1.Unit, request.Q1.MeasurementType);
                var q2Dto = new QuantityDTO(request.Q2.Value, request.Q2.Unit, request.Q2.MeasurementType);

                var result = _service.Add(q1Dto, q2Dto);

                var response = new ArithmeticOperationResponse
                {
                    Result = result.Value,
                    Unit = result.Unit,
                    MeasurementType = result.MeasurementType
                };

                return Ok(new ApiResponse<ArithmeticOperationResponse>(true, "Addition successful", response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in add endpoint");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] BinaryOperationRequest request, [FromQuery] bool isAuthenticated)
        {
            try
            {
                _logger.LogInformation($"Subtract request received - Authenticated: {isAuthenticated}");

                if (request?.Q1 == null || request?.Q2 == null)
                    return BadRequest(new ErrorResponse("Q1 and Q2 are required"));

                var q1Dto = new QuantityDTO(request.Q1.Value, request.Q1.Unit, request.Q1.MeasurementType);
                var q2Dto = new QuantityDTO(request.Q2.Value, request.Q2.Unit, request.Q2.MeasurementType);

                var result = _service.Subtract(q1Dto, q2Dto);

                var response = new ArithmeticOperationResponse
                {
                    Result = result.Value,
                    Unit = result.Unit,
                    MeasurementType = result.MeasurementType
                };

                return Ok(new ApiResponse<ArithmeticOperationResponse>(true, "Subtraction successful", response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in subtract endpoint");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] BinaryOperationRequest request, [FromQuery] bool isAuthenticated)
        {
            try
            {
                _logger.LogInformation($"Divide request received - Authenticated: {isAuthenticated}");

                if (request?.Q1 == null || request?.Q2 == null)
                    return BadRequest(new ErrorResponse("Q1 and Q2 are required"));

                var q1Dto = new QuantityDTO(request.Q1.Value, request.Q1.Unit, request.Q1.MeasurementType);
                var q2Dto = new QuantityDTO(request.Q2.Value, request.Q2.Unit, request.Q2.MeasurementType);

                double result = _service.Divide(q1Dto, q2Dto);

                var response = new DivisionResponse
                {
                    Result = result
                };

                return Ok(new ApiResponse<DivisionResponse>(true, "Division successful", response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in divide endpoint");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            try
            {
                _logger.LogInformation("History request received");
                // History would be stored in a database or message queue
                // For now, return empty history
                var history = new List<OperationHistoryResponse>();
                return Ok(new ApiResponse<List<OperationHistoryResponse>>(true, "History retrieved", history));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in history endpoint");
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }
    }
}
