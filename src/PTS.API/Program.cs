using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using PTS.API.Common.Response;
using PTS.API.Middleware;
using PTS.API.Services;
using PTS.API.Swagger;
using PTS.Application;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Localization.Shared;
using PTS.Application.Common.Response;
using PTS.Application.Features.Products.CreateProduct;
using PTS.Infrastructure;
using PTS.Infrastructure.Persistence.Seed;
using PTS.Infrastructure.Persistence.Seedings;

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
var jwtKey =
    builder.Configuration["Jwt:SigningKey"]
    ?? throw new InvalidOperationException("JWT signing key is missing.");

builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),

            RoleClaimType = "role", // where roles are stored
            NameClaimType = JwtRegisteredClaimNames.Sub, // optional, for User.Identity.Name
        };
        options.MapInboundClaims = false; // keep "sub" and "email" as written
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplication();
builder
    .Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var localizer = context.HttpContext.RequestServices.GetRequiredService<
                IStringLocalizer<SharedResource>
            >();
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
                localizer["ValidationErrors"].Value,
                details
            );

            return new BadRequestObjectResult(ApiResponse<object>.Fail(error));
        };
    });
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// Swagger services
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AcceptLanguageOperationFilter>();
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your JWT access token.",
        }
    );
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>(),
    });
});

// Localization services
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ne") };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("ne");

    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders = [new AcceptLanguageHeaderRequestCultureProvider()];
});

builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

var app = builder.Build();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    // var seeder = scope.ServiceProvider.GetRequiredService<>();
    // await seeder.SeedAsync();
    var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await seeder.SeedAsync();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
