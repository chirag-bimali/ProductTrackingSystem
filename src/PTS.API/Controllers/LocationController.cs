using Microsoft.AspNetCore.Mvc;
using PTS.API.Controllers;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.Locations.Queries.GetAllLocationQuery;
using DistrictQueries = PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;
using MunicipalityQueries = PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;
using ProvinceQueries = PTS.Application.Features.Locations.Queries.GetAllProvinceQuery;
using WardQueries = PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

public class LocationController(
    IQueryHandler<GetAllLocationQuery, GetAllLocationQueryResult> getAllLocationQueryHandler,
    IQueryHandler<
        ProvinceQueries.GetAllProvinceQuery,
        List<ProvinceQueries.ProvinceResult>
    > getAllProvinceQueryHandler,
    IQueryHandler<
        DistrictQueries.GetAllDistrictByProvinceIdQuery,
        List<DistrictQueries.DistrictResult>
    > getAllDistrictQueryHandler,
    IQueryHandler<
        MunicipalityQueries.GetAllMunicipalityByDistrictIdQuery,
        List<MunicipalityQueries.MunicipalityResult>
    > getAllMunicipalityQueryHandler,
    IQueryHandler<
        WardQueries.GetAllWardByMunicipalityIdQuery,
        List<WardQueries.WardResult>
    > getAllWardQueryHandler
) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllLocation(CancellationToken ct)
    {
        var result = await getAllLocationQueryHandler.Handle(new GetAllLocationQuery(), ct);
        return FromResult(result);
    }

    [HttpGet("Provinces")]
    public async Task<IActionResult> GetAllProvinces(CancellationToken ct)
    {
        var result = await getAllProvinceQueryHandler.Handle(
            new ProvinceQueries.GetAllProvinceQuery(),
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
        var result = await getAllDistrictQueryHandler.Handle(
            new DistrictQueries.GetAllDistrictByProvinceIdQuery(provinceId),
            ct
        );

        return FromResult(result);
    }

    [HttpGet("Districts/{districtId:int}/Municipalities")]
    public async Task<IActionResult> GetAllMunicipalities(
        [FromRoute] int districtId,
        CancellationToken ct
    )
    {
        var result = await getAllMunicipalityQueryHandler.Handle(
            new MunicipalityQueries.GetAllMunicipalityByDistrictIdQuery(districtId),
            ct
        );

        return FromResult(result);
    }

    [HttpGet("Municipalities/{municipalityId:int}/Wards")]
    public async Task<IActionResult> GetAllWards(
        [FromRoute] int municipalityId,
        CancellationToken ct
    )
    {
        var result = await getAllWardQueryHandler.Handle(
            new WardQueries.GetAllWardByMunicipalityIdQuery(municipalityId),
            ct
        );

        return FromResult(result);
    }
}
