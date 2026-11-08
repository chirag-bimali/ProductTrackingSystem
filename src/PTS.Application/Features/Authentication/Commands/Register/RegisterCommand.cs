using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Authentication.Commands.Register;

[RequireNoAuthentication]
[RequirePermission(Permissions.No.Permission)]
public record RegisterCommand(
    string NameEn,
    string NameNp,
    string Contact,
    string Email,
    string Password,
    string Role // "Farmer" or "FactoryAdmin"
) : ICommand<RegisterResult>;

public record RegisterResult(Guid UserId);
