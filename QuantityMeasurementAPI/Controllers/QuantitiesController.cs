using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementModelLayer.Models.Request;
using QuantityMeasurementModelLayer.Models.Response;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.DTO;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace QuantityMeasurementAPI.Controllers
{
    /// <summary>
    /// Quantity Measurement API Controller
    /// Handles all measurement operations via REST API endpoints
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class QuantitiesController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly ILogger<QuantitiesController> _logger;

        public QuantitiesController(IQuantityMeasurementService service, ILogger<QuantitiesController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Compare two quantities
        /// </summary>
        /// <param name="request">Two quantities to compare</param>
        /// <returns>Comparison result (AreEqual boolean)</returns>
        /// <response code="200">Comparison successful</response>
        /// <response code="400">Invalid input or measurement type mismatch</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("compare")]
        [ProducesResponseType(typeof(ApiResponse<ComparisonResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Compare([FromBody] BinaryOperationRequest request)
        {
            _logger.LogInformation("Compare operation called");

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

        /// <summary>
        /// Convert a quantity from one unit to another
        /// </summary>
        /// <param name="request">Quantity and target unit</param>
        /// <returns>Converted quantity</returns>
        /// <response code="200">Conversion successful</response>
        /// <response code="400">Invalid input or unit not supported</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(ApiResponse<ConversionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            _logger.LogInformation("Convert operation called");

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

        /// <summary>
        /// Add two quantities
        /// </summary>
        /// <param name="request">Two quantities to add</param>
        /// <returns>Sum of the quantities</returns>
        /// <response code="200">Addition successful</response>
        /// <response code="400">Invalid input or measurement type mismatch</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ApiResponse<ArithmeticOperationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] BinaryOperationRequest request)
        {
            _logger.LogInformation("Add operation called");

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

        /// <summary>
        /// Subtract second quantity from first
        /// </summary>
        /// <param name="request">Two quantities to subtract</param>
        /// <returns>Difference of the quantities</returns>
        /// <response code="200">Subtraction successful</response>
        /// <response code="400">Invalid input or measurement type mismatch</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("subtract")]
        [ProducesResponseType(typeof(ApiResponse<ArithmeticOperationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Subtract([FromBody] BinaryOperationRequest request)
        {
            _logger.LogInformation("Subtract operation called");

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

        /// <summary>
        /// Divide first quantity by second
        /// </summary>
        /// <param name="request">Two quantities to divide</param>
        /// <returns>Division result (scalar)</returns>
        /// <response code="200">Division successful</response>
        /// <response code="400">Invalid input or measurement type mismatch</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("divide")]
        [ProducesResponseType(typeof(ApiResponse<DivisionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Divide([FromBody] BinaryOperationRequest request)
        {
            _logger.LogInformation("Divide operation called");

            var q1Dto = new QuantityDTO(request.Q1.Value, request.Q1.Unit, request.Q1.MeasurementType);
            var q2Dto = new QuantityDTO(request.Q2.Value, request.Q2.Unit, request.Q2.MeasurementType);

            double result = _service.Divide(q1Dto, q2Dto);

            var response = new DivisionResponse
            {
                Result = result
            };

            return Ok(new ApiResponse<DivisionResponse>(true, "Division successful", response));
        }

        /// <summary>
        /// Get operation history
        /// </summary>
        /// <returns>List of all measurement operations</returns>
        /// <response code="200">History retrieved successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("history")]
        [ProducesResponseType(typeof(ApiResponse<List<OperationHistoryResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetHistory()
        {
            _logger.LogInformation("GetHistory operation called");

            var history = _service.GetHistory();

            var responses = history.Select(h => new OperationHistoryResponse
            {
                Id = h.Id,
                Operation = h.Operation.ToString(),
                Operand1 = h.Operand1,
                Operand2 = h.Operand2,
                Result = h.Result,
                HasError = h.HasError,
                ErrorMessage = h.ErrorMessage,
                CreatedAt = h.CreatedAt
            }).ToList();

            return Ok(new ApiResponse<List<OperationHistoryResponse>>(true, "History retrieved successfully", responses));
        }

        /// <summary>
        /// Get total count of operations
        /// </summary>
        /// <returns>Number of operations performed</returns>
        /// <response code="200">Count retrieved successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("count")]
        [ProducesResponseType(typeof(ApiResponse<CountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult GetCount()
        {
            _logger.LogInformation("GetCount operation called");

            var count = _service.GetHistory().Count();

            var response = new CountResponse
            {
                TotalOperations = count
            };

            return Ok(new ApiResponse<CountResponse>(true, "Count retrieved successfully", response));
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        /// <returns>API status</returns>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "API is running", timestamp = DateTime.UtcNow });
        }
    }
}
