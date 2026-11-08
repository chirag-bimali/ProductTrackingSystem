using System.Reflection;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Common.Security;
using SE.Application.Abstractions.Security;

namespace PTS.Application.Abstractions.Behaviours.Common;

internal sealed class RequestAuthorization<TRequest, TResponse>(
    IPermissionService permissionService,
    ICurrentUser currentUser
)
    where TRequest : IRequest<TResponse>
{
    private static readonly string[] Required = typeof(TRequest)
        .GetCustomAttributes<RequirePermissionAttribute>()
        .Select(a => a.Permission)
        .ToArray();

    public async Task<Error?> AuthorizeAsync(CancellationToken ct)
    {
        if (Required.Any(p => p == Permissions.No.Permission))
            return null;

        if (!currentUser.IsAuthenticated)
            return Error.Unauthorized("auth.unauthenticated", "You must be logged in to perform this action.");

        foreach (var permission in Required)
        {
            if (permission == Permissions.No.Permission)
                continue;

            if (
                !await permissionService.HasPermissionAsync(
                    currentUser.Id ?? throw new Exception("Something went wrong."),
                    permission,
                    ct
                )
            )
                return Error.Forbidden(
                    "auth.forbidden",
                    "You do not have permission for this action."
                );
        }

        return null;
    }
}
