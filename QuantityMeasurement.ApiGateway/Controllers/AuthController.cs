using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.SharedModels.Models.Response;
using QuantityMeasurement.SharedModels.Models.Request;
using System.Net.Http.Headers;

namespace QuantityMeasurement.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthController> _logger;
        private readonly string _authServiceUrl;

        public AuthController(HttpClient httpClient, IConfiguration config, ILogger<AuthController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _authServiceUrl = config["Services:AuthService:Url"] ?? "http://localhost:5001";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding register request to Auth Service");
                var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/api/auth/register", request);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var content = await response.Content.ReadAsAsync<AuthResponse>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway register");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding login request to Auth Service");
                var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/api/auth/login", request);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var content = await response.Content.ReadAsAsync<AuthResponse>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway login");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                _logger.LogInformation("Gateway: Forwarding google-login request to Auth Service");
                var response = await _httpClient.PostAsJsonAsync($"{_authServiceUrl}/api/auth/google-login", request);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }

                var content = await response.Content.ReadAsAsync<AuthResponse>();
                return Ok(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in gateway google-login");
                return StatusCode(500, new ErrorResponse("Gateway error: " + ex.Message));
            }
        }
    }
}
