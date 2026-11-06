using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Common.Security;

namespace PTS.Application.Abstractions.Behaviours;

internal sealed class AuthenticationQueryDecorator<TQuery, TResponse>(
    IQueryHandler<TQuery, TResponse> inner,
    ICurrentUser currentUser
) : IQueryHandler<TQuery, TResponse>
{
    private static readonly bool RequiresNoAuthentication = typeof(TQuery).IsDefined(
        typeof(RequireNoAuthenticationAttribute),
        inherit: true
    );

    public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct)
    {
        if (RequiresNoAuthentication)
            return await inner.Handle(query, ct);

        if (currentUser.IsAuthenticated)
            return await inner.Handle(query, ct);

        return Error.Unauthorized("auth.unauthenticated", "You must be logged in");
    }
}
