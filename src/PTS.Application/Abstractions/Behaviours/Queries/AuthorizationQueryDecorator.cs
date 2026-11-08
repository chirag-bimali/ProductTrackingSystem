using PTS.Application.Abstractions.Behaviours.Common;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours.Queries;

internal sealed class AuthorizationQueryDecorator<TQuery, TResponse>(
    IQueryHandler<TQuery, TResponse> inner,
    RequestAuthorization<TQuery, TResponse> authorization
) : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct)
    {
        var error = await authorization.AuthorizeAsync(ct);

        if (error is not null)
            return error;

        return await inner.Handle(query, ct);
    }
}
