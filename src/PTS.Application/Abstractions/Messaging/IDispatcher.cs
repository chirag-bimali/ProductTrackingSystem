using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Messaging;

public interface IDispatcher
{
    Task<Result<TResponse>> DispatchQuery<TQuery, TResponse>(
        TQuery query,
        CancellationToken ct = default
    )
        where TQuery : IQuery<TResponse>;

    Task<Result<TResponse>> DispatchCommand<TCommand, TResponse>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResponse>;
}
