using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.AuthService.Entities;

namespace QuantityMeasurement.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
