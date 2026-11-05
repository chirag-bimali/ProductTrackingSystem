using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Users.Queries.GetAllLocalAdmin;

public class GetAllLocalAdminQueryHandler(ILocalAdminRepository localAdminRepository)
    : IQueryHandler<GetAllLocalAdminQuery, GetAllLocalAdminQueryResult>
{
    public async Task<Result<GetAllLocalAdminQueryResult>> Handle(
        GetAllLocalAdminQuery request,
        CancellationToken cancellationToken
    )
    {
        var (localAdmins, totalCount) = await localAdminRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken
        );

        var items = localAdmins
            .Select(localAdmin => new LocalAdminResult(
                localAdmin.UserId,
                localAdmin.User.NameEn,
                localAdmin.User.NameNp,
                localAdmin.User.Contact,
                localAdmin.User.Email,
                localAdmin.User.VerificationStatus,
                localAdmin.MunicipalityId
            ))
            .ToList();

        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

        return new GetAllLocalAdminQueryResult(
            items,
            request.PageNumber,
            request.PageSize,
            totalCount,
            totalPages
        );
    }
}
