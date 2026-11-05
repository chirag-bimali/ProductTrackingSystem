namespace PTS.Application.Features.Users.Queries.GetAllLocalAdmin;

public record GetAllLocalAdminQueryResult(
    IReadOnlyList<LocalAdminResult> Items,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages
);

public record LocalAdminResult(
    Guid UserId,
    string FirstName,
    string LastName,
    string Contact,
    string Email,
    bool VerificationStatus,
    int MunicipalityId
);
