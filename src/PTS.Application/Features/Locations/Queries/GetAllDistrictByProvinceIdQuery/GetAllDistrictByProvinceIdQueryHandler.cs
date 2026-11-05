using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;

public class GetAllDistrictByProvinceIdQueryHandler
    : IQueryHandler<GetAllDistrictByProvinceIdQuery, List<DistrictResult>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllDistrictByProvinceIdQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Result<List<DistrictResult>>> Handle(
        GetAllDistrictByProvinceIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var districts = await _locationRepository.GetAllDistrictsByProvinceIdAsync(
            request.ProvinceId,
            cancellationToken
        );

        return districts.Select(DistrictResult.FromEntity).ToList();
    }
}
