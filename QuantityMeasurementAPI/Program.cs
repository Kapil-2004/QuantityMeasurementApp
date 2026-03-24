using Microsoft.EntityFrameworkCore;
using QuantityMeasurementAPI.Data;
using QuantityMeasurementAPI.Middleware;
using QuantityMeasurementAPI.Repositories;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementRepositoryLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection - Service Layer
builder.Services.AddScoped<IQuantityMeasurementService, QuantityMeasurementServiceImpl>();

// Dependency Injection - Repository Layer (EF Core based)
builder.Services.AddScoped<IQuantityMeasurementRepository, EFCoreQuantityMeasurementRepository>();

// CORS Configuration (if needed for frontend integration)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        args =>
        {
            args.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Note: Database migrations are managed separately via: dotnet ef database update
// The QuantityMeasurementDB database with QuantityMeasurements table should already exist

// Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

// Global Exception Handler Middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
