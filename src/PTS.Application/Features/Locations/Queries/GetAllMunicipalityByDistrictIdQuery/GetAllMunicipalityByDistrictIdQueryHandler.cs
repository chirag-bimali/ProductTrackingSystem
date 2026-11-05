using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;

public class GetAllMunicipalityByDistrictIdQueryHandler
    : IQueryHandler<GetAllMunicipalityByDistrictIdQuery, List<MunicipalityResult>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllMunicipalityByDistrictIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Result<List<MunicipalityResult>>> Handle(
        GetAllMunicipalityByDistrictIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var municipalities = await _locationRepository.GetAllMunicipalitiesByDistrictIdAsync(
            request.DistrictId,
            cancellationToken
        );

        if (municipalities == null)
        {
            return Error.NotFound(
                "municipalities.not.found",
                "No municipalities found for the given district ID."
            );
        }

        return municipalities.Select(MunicipalityResult.FromEntity).ToList();
    }
}
