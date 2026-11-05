using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Abstractions.Security;
using PTS.Infrastructure.Persistence;
using PTS.Infrastructure.Persistence.Repositories;
using PTS.Infrastructure.Persistence.Seed;
using PTS.Infrastructure.Security;

namespace PTS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocalAdminRepository, LocalAdminRepository>();

        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        services.AddScoped<DbSeeder>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
