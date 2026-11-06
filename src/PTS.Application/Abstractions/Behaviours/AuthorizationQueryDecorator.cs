using System.Reflection;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Common.Security;
using SE.Application.Abstractions.Security;

namespace PTS.Application.Abstractions.Behaviours;

internal sealed class AuthorizationQueryDecorator<TQuery, TResponse>(
    IQueryHandler<TQuery, TResponse> inner,
    IPermissionService permissionService,
    ICurrentUser currentUser
) : IQueryHandler<TQuery, TResponse>
{
    private static readonly string[] Required = typeof(TQuery)
        .GetCustomAttributes<RequirePermissionAttribute>()
        .Select(a => a.Permission)
        .ToArray();

    public async Task<Result<TResponse>> Handle(TQuery command, CancellationToken ct)
    {
        if (Required.Any(p => p == Permissions.No.Permission))
            return await inner.Handle(command, ct);

        if (!currentUser.IsAuthenticated)
            return Error.Unauthorized("auth.unauthenticated", "You must be logged in");

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

        return await inner.Handle(command, ct);
    }
}
