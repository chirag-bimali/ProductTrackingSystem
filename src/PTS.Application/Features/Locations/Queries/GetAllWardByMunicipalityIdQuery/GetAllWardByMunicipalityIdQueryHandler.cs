using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

public class GetAllWardByMunicipalityIdQueryHandler
    : IQueryHandler<GetAllWardByMunicipalityIdQuery, List<WardResult>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllWardByMunicipalityIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Result<List<WardResult>>> Handle(
        GetAllWardByMunicipalityIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var wards = await _locationRepository.GetAllWardsByMunicipalityIdAsync(
            request.MunicipalityId,
            cancellationToken
        );

        if (wards == null)
        {
            return Error.NotFound(
                "wards.not.found",
                "No wards found for the given municipality ID."
            );
        }

        return wards.Select(WardResult.FromEntity).ToList();
    }
}
