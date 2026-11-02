using Microsoft.AspNetCore.Mvc;
using PTS.API.Common.Response;
using PTS.API.Middleware;
using PTS.Application;
using PTS.Application.Common.Response;
using PTS.Application.Features.Products.CreateProduct;
using PTS.Infrastructure;
using PTS.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Infrastructure services
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Database connection string 'DefaultConnection' was not found."
    );

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplication();

builder
    .Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var details = context
                .ModelState.Where(entry => entry.Value?.Errors.Count > 0)
                .ToDictionary(
                    entry => entry.Key,
                    entry =>
                        entry
                            .Value!.Errors.Select(error =>
                                string.IsNullOrWhiteSpace(error.ErrorMessage)
                                    ? error.Exception?.Message ?? "The value is invalid."
                                    : error.ErrorMessage
                            )
                            .ToArray()
                );

            var error = Error.Validation(
                "Validation.Failed",
                "One or more validation errors occurred.",
                details
            );

            return new BadRequestObjectResult(ApiResponse<object>.Fail(error));
        };
    });
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
