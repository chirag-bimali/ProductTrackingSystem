using System.Reflection;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Common.Security;

namespace PTS.Application.Abstractions.Behaviours;

internal sealed class AuthenticationCommandDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> inner,
    ICurrentUser currentUser
) : ICommandHandler<TCommand, TResponse>
{
    private static readonly bool RequiresNoAuthentication = typeof(TCommand).IsDefined(
        typeof(RequireNoAuthenticationAttribute),
        inherit: true
    );

    public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct)
    {
        if (RequiresNoAuthentication)
            return await inner.Handle(command, ct);

        if (currentUser.IsAuthenticated)
            return await inner.Handle(command, ct);

        return Error.Unauthorized("auth.unauthenticated", "You must be logged in");
    }
}
