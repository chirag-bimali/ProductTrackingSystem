using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Authentication.Commands.Login;

[RequireNoAuthentication]
[RequirePermission(Permissions.No.Permission)]
public record LoginCommand(string? Email, string? Contact, string Password)
    : ICommand<GeneratedToken>;
