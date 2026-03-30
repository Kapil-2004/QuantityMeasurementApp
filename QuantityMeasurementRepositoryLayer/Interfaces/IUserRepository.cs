using QuantityMeasurementModelLayer.Entities;
using System.Threading.Tasks;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByUsernameAsync(string username);
        Task<UserEntity> AddUserAsync(UserEntity user);
    }
}
