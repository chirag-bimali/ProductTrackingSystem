namespace PTS.Application.Features.Users.Commands.CreateLocalAdmin;

public record CreateLocalAdminCommand(
    string FirstName,
    string LastName,
    string Contact,
    string Email,
    string Password,
    int MunicipalityId
);
