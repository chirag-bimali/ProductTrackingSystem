using PTS.Application.Common.Security;

namespace PTS.Application.Features.Users.Commands.CreateLocalAdmin;

[RequirePermission(Permissions.LocalAdmin.Create)]
public record CreateLocalAdminCommand(
    string FirstName,
    string LastName,
    string Contact,
    string Email,
    string Password,
    int MunicipalityId
);
