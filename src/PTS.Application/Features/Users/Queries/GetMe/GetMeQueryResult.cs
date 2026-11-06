namespace PTS.Application.Features.Users.Queries.GetMe;
public record GetMeQueryResult(
    Guid Id,
    string Email,
    string NameEn,
    string NameNe
    // IReadOnlyCollection<string> Roles,
    // IReadOnlyCollection<string> Permissions
);