using PTS.Application.Abstractions.Behaviours.Common;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours.Queries;

internal sealed class AuthenticationQueryDecorator<TQuery, TResponse>(
    IQueryHandler<TQuery, TResponse> inner,
    RequestAuthentication<TQuery, TResponse> authentication
) : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct)
    {
        if (authentication.IsAuthorized())
            return inner.Handle(query, ct);

        return Task.FromResult<Result<TResponse>>(
            Error.Unauthorized("auth.unauthenticated", "You must be logged in")
        );
    }
}
