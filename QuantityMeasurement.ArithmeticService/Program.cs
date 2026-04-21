using Microsoft.OpenApi.Models;
using QuantityMeasurement.ArithmeticService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Arithmetic Service", Version = "v1" });
});

// Services
builder.Services.AddScoped<IArithmeticService, ArithmeticServiceImpl>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowServices", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5000",
                "http://localhost:5001",
                "http://localhost:5002",
                "http://localhost:5003",
                "http://localhost:5004",
                "http://localhost:4200",
                "http://localhost:3000"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowServices");
app.MapControllers();

app.Run();
