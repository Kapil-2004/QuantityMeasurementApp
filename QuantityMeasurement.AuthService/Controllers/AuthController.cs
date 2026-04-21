using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.AuthService.Services;
using QuantityMeasurement.SharedModels.Models.Auth;
using QuantityMeasurement.SharedModels.Models.Response;
using System.Net.Http.Headers;

namespace QuantityMeasurement.AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation($"Register request for email: {request.Email}");
                
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorResponse("Invalid model state", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList()));

                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in register");
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                _logger.LogInformation($"Login request for email: {request.Email}");
                
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorResponse("Invalid model state"));

                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in login");
                return Unauthorized(new ErrorResponse(ex.Message));
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                _logger.LogInformation("Google login request received");
                
                if (!ModelState.IsValid)
                    return BadRequest(new ErrorResponse("Invalid model state"));

                var response = await _authService.GoogleLoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in google login");
                return Unauthorized(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("validate-token")]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                    return Unauthorized(new TokenValidationResponse { IsValid = false, Message = "No token provided" });

                var token = authHeader.Replace("Bearer ", "").Trim();
                var response = await _authService.ValidateTokenAsync(token);
                
                if (!response.IsValid)
                    return Unauthorized(response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in validate-token");
                return Unauthorized(new TokenValidationResponse { IsValid = false, Message = ex.Message });
            }
        }
    }
}
