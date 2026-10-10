using MediatR;

namespace PTS.Application.Features.Authorization.Command.Register;

public record RegisterCommand(
    string NameEn,
    string NameNp,
    string Contact,
    string Email,
    string Password,
    string Role,          // "Farmer" or "FactoryAdmin"
    int WardId,
    string? AddressLine = null,
    string? GpsCoordinates = null,
    string? ImageUrl = null) : IRequest<RegisterResult>;

public record RegisterResult(Guid UserId, string Email, string Role, bool IsVerified);