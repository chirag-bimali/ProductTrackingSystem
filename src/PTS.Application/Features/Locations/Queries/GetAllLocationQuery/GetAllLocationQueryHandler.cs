using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Locations.Queries.GetAllLocationQuery;

public sealed class GetAllLocationQueryHandler(
    ILocationRepository locationRepository)
    : IQueryHandler<GetAllLocationQuery, GetAllLocationQueryResult>
{
    public async Task<Result<GetAllLocationQueryResult>> Handle(
        GetAllLocationQuery query,
        CancellationToken ct)
    {
        var provinces = await locationRepository.GetAllHierarchyAsync(ct);

        return GetAllLocationQueryResult.FromEntities(provinces);
    }
}