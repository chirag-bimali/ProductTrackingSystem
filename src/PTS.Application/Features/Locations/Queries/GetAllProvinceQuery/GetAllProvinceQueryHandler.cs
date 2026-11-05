using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Locations.Queries.GetAllProvincesQuery;

public class GetAllProvincesQueryHandler : IQueryHandler<GetAllProvinceQuery, List<ProvinceResult>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllProvincesQueryHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<Result<List<ProvinceResult>>> Handle(
        GetAllProvinceQuery request,
        CancellationToken cancellationToken
    )
    {
        var provinces = await _locationRepository.GetAllProvincesAsync(cancellationToken);

        return provinces.Select(ProvinceResult.FromEntity).ToList();
    }
}
