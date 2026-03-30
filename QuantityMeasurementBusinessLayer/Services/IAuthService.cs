using QuantityMeasurementModelLayer.Models.Auth;
using System.Threading.Tasks;

namespace QuantityMeasurementBusinessLayer.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> GoogleLoginAsync(GoogleLoginRequest request);
    }
}
