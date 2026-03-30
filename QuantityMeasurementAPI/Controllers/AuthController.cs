using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementModelLayer.Models.Auth;
using System.Threading.Tasks;

namespace QuantityMeasurementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authService.RegisterAsync(request);
                return Ok(response);
            }
            catch (QuantityMeasurementException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (QuantityMeasurementException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authService.GoogleLoginAsync(request);
                return Ok(response);
            }
            catch (QuantityMeasurementException ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
    }
}
