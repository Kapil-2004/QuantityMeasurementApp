using QuantityMeasurement.SharedModels.Models.Auth;
using QuantityMeasurement.SharedModels.Models.Response;
using System.Threading.Tasks;

namespace QuantityMeasurement.AuthService.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> GoogleLoginAsync(GoogleLoginRequest request);
        Task<TokenValidationResponse> ValidateTokenAsync(string token);
    }
}
