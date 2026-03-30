using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurementBusinessLayer.Exceptions;
using QuantityMeasurementBusinessLayer.Services.Security;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Models.Auth;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthServiceImpl(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if user exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (existingUser != null)
                throw new QuantityMeasurementException("Username already exists.");

            // Hash password
            var passwordHash = SecurityHelper.HashPassword(request.Password);

            // Create user
            var user = new UserEntity
            {
                Username = request.Username,
                PasswordHash = passwordHash
            };

            await _userRepository.AddUserAsync(user);

            // Generate token
            return GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            // Get user
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null)
                throw new QuantityMeasurementException("Invalid credentials.");

            // Verify password
            bool isPasswordValid = SecurityHelper.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new QuantityMeasurementException("Invalid credentials.");

            // Generate token
            return GenerateAuthResponse(user);
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

                // Find user by email (we'll map Google Email to Username)
                var user = await _userRepository.GetUserByUsernameAsync(payload.Email);
                if (user == null)
                {
                    // Create a new user mapping Username to Google Email
                    // Use a placeholder or empty password since they authenticate via Google
                    user = new UserEntity
                    {
                        Username = payload.Email,
                        PasswordHash = "GOOGLE_OAUTH_LOGIN"
                    };
                    await _userRepository.AddUserAsync(user);
                }

                return GenerateAuthResponse(user);
            }
            catch (InvalidJwtException)
            {
                throw new QuantityMeasurementException("Invalid Google token.");
            }
            catch (Exception)
            {
                throw new QuantityMeasurementException("Failed to verify Google login.");
            }
        }

        private AuthResponse GenerateAuthResponse(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = _configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing");
            var key = Encoding.UTF8.GetBytes(keyString);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:DurationInMinutes"] ?? "60")),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                Token = tokenHandler.WriteToken(token),
                Username = user.Username,
                Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
