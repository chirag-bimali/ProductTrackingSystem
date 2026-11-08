using PTS.Application.Abstractions.Behaviours.Common;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours.Commands;

internal sealed class AuthenticationCommandDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> inner,
    RequestAuthentication<TCommand, TResponse> authentication
) : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct)
    {
        if (authentication.IsAuthorized())
            return inner.Handle(command, ct);

        return Task.FromResult<Result<TResponse>>(
            Error.Unauthorized("auth.unauthenticated", "You must be logged in")
        );
    }
}
