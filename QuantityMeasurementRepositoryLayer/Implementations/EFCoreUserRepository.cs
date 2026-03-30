using Microsoft.EntityFrameworkCore;
using QuantityMeasurementRepositoryLayer.Data;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;
using System.Threading.Tasks;

namespace QuantityMeasurementRepositoryLayer.Implementations
{
    public class EFCoreUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCoreUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<UserEntity> AddUserAsync(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
