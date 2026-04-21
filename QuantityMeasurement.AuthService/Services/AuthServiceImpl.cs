using Microsoft.IdentityModel.Tokens;
using QuantityMeasurement.AuthService.Entities;
using QuantityMeasurement.AuthService.Repositories;
using QuantityMeasurement.AuthService.Services.Security;
using QuantityMeasurement.SharedModels.Exceptions;
using QuantityMeasurement.SharedModels.Models.Auth;
using QuantityMeasurement.SharedModels.Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;

namespace QuantityMeasurement.AuthService.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthServiceImpl> _logger;

        public AuthServiceImpl(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthServiceImpl> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                    throw new QuantityMeasurementException("Email and password are required.");

                // Check if user exists
                var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (existingUser != null)
                    throw new QuantityMeasurementException("Email already registered.");

                // Hash password
                var passwordHash = SecurityHelper.HashPassword(request.Password);

                // Create user
                var user = new UserEntity
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    Name = request.Name ?? ""
                };

                await _userRepository.AddUserAsync(user);
                _logger.LogInformation($"User registered: {request.Email}");

                return GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                throw;
            }
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                // Get user
                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                    throw new QuantityMeasurementException("Invalid credentials.");

                // Verify password
                bool isPasswordValid = SecurityHelper.VerifyPassword(request.Password, user.PasswordHash);
                if (!isPasswordValid)
                    throw new QuantityMeasurementException("Invalid credentials.");

                _logger.LogInformation($"User logged in: {request.Email}");

                return GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                throw;
            }
        }

        public async Task<AuthResponse> GoogleLoginAsync(GoogleLoginRequest request)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _configuration["Authentication:Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

                // Find user by email
                var user = await _userRepository.GetUserByEmailAsync(payload.Email);
                if (user == null)
                {
                    // Create a new user
                    user = new UserEntity
                    {
                        Email = payload.Email,
                        PasswordHash = "GOOGLE_OAUTH_LOGIN",
                        Name = payload.Name ?? ""
                    };
                    await _userRepository.AddUserAsync(user);
                    _logger.LogInformation($"New Google user created: {payload.Email}");
                }

                _logger.LogInformation($"User logged in via Google: {payload.Email}");

                return GenerateAuthResponse(user);
            }
            catch (InvalidJwtException)
            {
                _logger.LogError("Invalid Google token");
                throw new QuantityMeasurementException("Invalid Google token.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Google login");
                throw new QuantityMeasurementException("Failed to verify Google login.");
            }
        }

        public async Task<TokenValidationResponse> ValidateTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return new TokenValidationResponse { IsValid = false, Message = "Token is empty" };

                var tokenHandler = new JwtSecurityTokenHandler();
                var keyString = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");
                var key = Encoding.UTF8.GetBytes(keyString);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var emailClaim = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (int.TryParse(userIdClaim, out int userId))
                {
                    _logger.LogInformation($"Token validated for user: {userId}");
                    return new TokenValidationResponse
                    {
                        IsValid = true,
                        UserId = userId,
                        Email = emailClaim ?? "",
                        Message = "Token is valid"
                    };
                }

                return new TokenValidationResponse { IsValid = false, Message = "Invalid token claims" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation failed");
                return new TokenValidationResponse { IsValid = false, Message = ex.Message };
            }
        }

        private AuthResponse GenerateAuthResponse(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");
            var key = Encoding.UTF8.GetBytes(keyString);
            var durationInMinutes = double.Parse(_configuration["Jwt:DurationInMinutes"] ?? "60");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name ?? user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthResponse
            {
                Success = true,
                Message = "Authentication successful",
                Token = tokenString,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name ?? ""
                }
            };
        }
    }
}
