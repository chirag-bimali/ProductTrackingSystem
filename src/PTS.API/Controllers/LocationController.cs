using Microsoft.AspNetCore.Mvc;
using PTS.API.Controllers;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;
using PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;
using PTS.Application.Features.Locations.Queries.GetAllProvinceQuery;
using PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

public class LocationController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpGet("Provinces")]
    public async Task<IActionResult> GetAllProvinces(CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<GetAllProvinceQuery, List<ProvinceResult>>(
            new GetAllProvinceQuery(),
            ct
        );

        return FromResult(result);
    }

    [HttpGet("Provinces/{provinceId:int}/Districts")]
    public async Task<IActionResult> GetAllDistricts(
        [FromRoute] int provinceId,
        CancellationToken ct
    )
    {
        var result = await dispatcher.DispatchQuery<
            GetAllDistrictByProvinceIdQuery,
            List<DistrictResult>
        >(new GetAllDistrictByProvinceIdQuery(provinceId), ct);

        return FromResult(result);
    }

    [HttpGet("Districts/{districtId:int}/Municipalities")]
    public async Task<IActionResult> GetAllMunicipalities(
        [FromRoute] int districtId,
        CancellationToken ct
    )
    {
        var result = await dispatcher.DispatchQuery<
            GetAllMunicipalityByDistrictIdQuery,
            List<MunicipalityResult>
        >(new GetAllMunicipalityByDistrictIdQuery(districtId), ct);

        return FromResult(result);
    }

    [HttpGet("Municipalities/{municipalityId:int}/Wards")]
    public async Task<IActionResult> GetAllWards(
        [FromRoute] int municipalityId,
        CancellationToken ct
    )
    {
        var result = await dispatcher.DispatchQuery<
            GetAllWardByMunicipalityIdQuery,
            List<WardResult>
        >(new GetAllWardByMunicipalityIdQuery(municipalityId), ct);

        return FromResult(result);
    }
}
