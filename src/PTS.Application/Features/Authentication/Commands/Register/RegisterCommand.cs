using PTS.Application.Common.Response;

namespace PTS.Application.Features.Authorization.Command.Register;

public record RegisterCommand(
    string NameEn,
    string NameNp,
    string Contact,
    string Email,
    string Password,
    string Role // "Farmer" or "FactoryAdmin"
);

public record RegisterResult(Guid UserId, string Email, string Role, bool IsVerified);
