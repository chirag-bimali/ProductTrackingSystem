using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand, TResult>
{
    Task<Result<TResult>> Handle(TCommand command, CancellationToken ct);
}