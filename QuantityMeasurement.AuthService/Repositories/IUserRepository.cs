using QuantityMeasurement.AuthService.Entities;

namespace QuantityMeasurement.AuthService.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task<UserEntity> AddUserAsync(UserEntity user);
        Task SaveChangesAsync();
    }
}
