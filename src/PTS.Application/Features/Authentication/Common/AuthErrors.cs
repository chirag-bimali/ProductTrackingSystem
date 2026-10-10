using PTS.Application.Common.Response;

namespace PTS.Application.Features.Authorization.Common;

public static class AuthErrors
{
    public static readonly Error InvalidCredentials =
        Error.Unauthorized("Auth.InvalidCredentials", "Invalid email/contact or password.");

    public static Error RoleNotFound(string role) =>
        Error.Validation("Auth.RoleNotFound", $"Role '{role}' does not exist.");
}