using System.Reflection;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Common.Security;

namespace PTS.Application.Abstractions.Behaviours.Common;

internal sealed class RequestAuthentication<TRequest, TResponse>(ICurrentUser currentUser)
    where TRequest : IRequest<TResponse>
{
    private static readonly bool RequiresNoAuthentication = typeof(TRequest).IsDefined(
        typeof(RequireNoAuthenticationAttribute),
        inherit: true
    );

    public bool IsAuthorized()
    {
        return RequiresNoAuthentication || currentUser.IsAuthenticated;
    }
}
