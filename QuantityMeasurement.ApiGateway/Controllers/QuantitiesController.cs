using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.SharedModels.Models.Response;
using QuantityMeasurement.SharedModels.Models.Request;
using System.Net.Http.Headers;

namespace QuantityMeasurement.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuantitiesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<QuantitiesController> _logger;
        private readonly string _compareServiceUrl;
        private readonly string _convertServiceUrl;
        private readonly string _arithmeticServiceUrl;
        private readonly string _authServiceUrl;

        public QuantitiesController(HttpClient httpClient, IConfiguration config, ILogger<QuantitiesController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _compareServiceUrl = config["Services:CompareService:Url"] ?? "http://localhost:5002";
            _convertServiceUrl = config["Services:ConvertService:Url"] ?? "http://localhost:5003";
            _arithmeticServiceUrl = config["Services:ArithmeticService:Url"] ?? "http://localhost:5004";
            _authServiceUrl = config["Services:AuthService:Url"] ?? "http://localhost:5001";
        }

        private async Task<bool> ValidateToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_authServiceUrl}/api/auth/validate-token");
                request.Headers.Authorization = AuthenticationHeaderValue.Parse(token);
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromBody] BinaryOperationRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding compare request to Compare Service");
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
                
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_compareServiceUrl}/api/quantities/compare?isAuthenticated={isAuthenticated}", 
                    request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<ComparisonResponse>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway compare");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] ConversionRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding convert request to Convert Service");
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
                
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_convertServiceUrl}/api/quantities/convert?isAuthenticated={isAuthenticated}", 
                    request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<ConversionResponse>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway convert");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] BinaryOperationRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding add request to Arithmetic Service");
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
                
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_arithmeticServiceUrl}/api/quantities/add?isAuthenticated={isAuthenticated}", 
                    request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<ArithmeticOperationResponse>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway add");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("subtract")]
        public async Task<IActionResult> Subtract([FromBody] BinaryOperationRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding subtract request to Arithmetic Service");
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
                
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_arithmeticServiceUrl}/api/quantities/subtract?isAuthenticated={isAuthenticated}", 
                    request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<ArithmeticOperationResponse>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway subtract");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("divide")]
        public async Task<IActionResult> Divide([FromBody] BinaryOperationRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding divide request to Arithmetic Service");
                var isAuthenticated = User.Identity?.IsAuthenticated ?? false;
                
                var response = await _httpClient.PostAsJsonAsync(
                    $"{_arithmeticServiceUrl}/api/quantities/divide?isAuthenticated={isAuthenticated}", 
                    request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<DivisionResponse>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway divide");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding history request to Compare/Convert/Arithmetic Services");
                var token = Request.Headers["Authorization"].ToString();
                
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_arithmeticServiceUrl}/api/quantities/history");
                if (!string.IsNullOrEmpty(token))
                    request.Headers.Authorization = AuthenticationHeaderValue.Parse(token);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

                var content = await response.Content.ReadAsAsync<ApiResponse<List<OperationHistoryResponse>>>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway history");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }
    }
}
