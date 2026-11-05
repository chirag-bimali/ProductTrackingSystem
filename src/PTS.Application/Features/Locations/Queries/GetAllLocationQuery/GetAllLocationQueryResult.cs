using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllLocationQuery;

public record GetAllLocationQueryResult(
    IReadOnlyList<ProvinceResult> Provinces)
{
    public static GetAllLocationQueryResult FromEntities(IEnumerable<Province> provinces) =>
        new(provinces.Select(ProvinceResult.FromEntity).ToList());
}

public record ProvinceResult(
    int Id,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website,
    string? HeadquarterEn,
    string? HeadquarterNe,
    IReadOnlyList<DistrictResult> Districts)
{
    public static ProvinceResult FromEntity(Province province) =>
        new(
            province.Id,
            province.NameEn,
            province.NameNp,
            province.AreaSqKm,
            province.Website,
            province.HeadquarterEn,
            province.HeadquarterNe,
            province.Districts.Select(DistrictResult.FromEntity).ToList());
}

public record DistrictResult(
    int Id,
    int ProvinceId,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website,
    string? HeadquarterEn,
    string? HeadquarterNe,
    IReadOnlyList<MunicipalityResult> Municipalities)
{
    public static DistrictResult FromEntity(District district) =>
        new(
            district.Id,
            district.ProvinceId,
            district.NameEn,
            district.NameNp,
            district.AreaSqKm,
            district.Website,
            district.HeadquarterEn,
            district.HeadquarterNe,
            district.Municipalities.Select(MunicipalityResult.FromEntity).ToList());
}

public record MunicipalityResult(
    int Id,
    int DistrictId,
    int CategoryId,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website,
    IReadOnlyList<WardResult> Wards)
{
    public static MunicipalityResult FromEntity(Municipality municipality) =>
        new(
            municipality.Id,
            municipality.DistrictId,
            municipality.CategoryId,
            municipality.NameEn,
            municipality.NameNp,
            municipality.AreaSqKm,
            municipality.Website,
            municipality.Wards.Select(WardResult.FromEntity).ToList());
}

public record WardResult(
    int Id,
    int MunicipalityId,
    int Number)
{
    public static WardResult FromEntity(Ward ward) =>
        new(ward.Id, ward.MunicipalityId, ward.Number);
}
