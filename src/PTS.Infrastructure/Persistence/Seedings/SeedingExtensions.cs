using Microsoft.Extensions.DependencyInjection;

namespace PTS.Infrastructure.Persistence.Seedings;

public static class SeedingExtensions
{
    public static IServiceCollection AddSeeders(this IServiceCollection services)
    {
        var seederTypes = typeof(SeedingExtensions).Assembly
            .GetTypes()
            .Where(t => typeof(ISeeder).IsAssignableFrom(t)
                        && t is { IsClass: true, IsAbstract: false });

        foreach (var type in seederTypes)
            services.AddScoped(typeof(ISeeder), type);

        services.AddScoped<DatabaseSeeder>();
        return services;
    }
}