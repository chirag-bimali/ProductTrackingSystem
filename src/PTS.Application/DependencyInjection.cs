using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PTS.Application.Abstractions.Behaviours;
using PTS.Application.Abstractions.Messaging;

namespace PTS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.Scan(scan =>
            scan.FromAssemblies(assembly)
                .AddClasses(c =>
                    c.AssignableTo(typeof(ICommandHandler<,>))
                        .Where(t => !t.IsGenericTypeDefinition)
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c =>
                    c.AssignableTo(typeof(IQueryHandler<,>)).Where(t => !t.IsGenericTypeDefinition)
                )
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        services.AddValidatorsFromAssembly(assembly);

        // services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingQueryDecorator<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandDecorator<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(AuthorizationCommandDecorator<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(AuthenticationCommandDecorator<,>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationQueryDecorator<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(AuthorizationQueryDecorator<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(AuthenticationQueryDecorator<,>));

        return services;
    }
}
