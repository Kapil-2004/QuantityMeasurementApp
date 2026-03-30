using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementModelLayer.Models;
using System.Text.Json;

namespace QuantityMeasurementRepositoryLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure QuantityMeasurementEntity
            var entityBuilder = modelBuilder.Entity<QuantityMeasurementEntity>();

            // Set up primary key
            entityBuilder.HasKey(e => e.Id);

            // Configure Id to auto-increment
            entityBuilder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Map complex object properties to JSON
            entityBuilder.Property(e => e.Operand1)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<QuantityModel<object>>(v, new JsonSerializerOptions()),
                    new ValueComparer<QuantityModel<object>>(
                        (c1, c2) => c1.Value == c2.Value && c1.Unit.Equals(c2.Unit),
                        c => HashCode.Combine(c.Value, c.Unit),
                        c => new QuantityModel<object>(c.Value, c.Unit)))
                .HasColumnType("nvarchar(max)");

            entityBuilder.Property(e => e.Operand2)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<QuantityModel<object>>(v, new JsonSerializerOptions()),
                    new ValueComparer<QuantityModel<object>>(
                        (c1, c2) => c1.Value == c2.Value && c1.Unit.Equals(c2.Unit),
                        c => HashCode.Combine(c.Value, c.Unit),
                        c => new QuantityModel<object>(c.Value, c.Unit)))
                .HasColumnType("nvarchar(max)");

            entityBuilder.Property(e => e.Result)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<object>(v, new JsonSerializerOptions()),
                    new ValueComparer<object>(
                        (c1, c2) => c1.Equals(c2),
                        c => c.GetHashCode(),
                        c => c))
                .HasColumnType("nvarchar(max)");

            entityBuilder.Property(e => e.Operation)
                .HasConversion(new EnumToStringConverter<QuantityMeasurementModelLayer.Enums.OperationType>())
                .HasMaxLength(50);

            entityBuilder.Property(e => e.ErrorMessage)
                .IsRequired(false)
                .HasMaxLength(500);
        }
    }
}
