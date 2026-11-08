using Microsoft.Extensions.DependencyInjection;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Response;

namespace PTS.Application.Common.Messaging;

public sealed class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public Task<Result<TResponse>> DispatchQuery<TQuery, TResponse>(
        TQuery query,
        CancellationToken ct = default
    )
        where TQuery : IQuery<TResponse>
    {
        var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();

        return handler.Handle(query, ct);
    }

    public Task<Result<TResponse>> DispatchCommand<TCommand, TResponse>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResponse>
    {
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResponse>>();

        return handler.Handle(command, ct);
    }
}
