using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;

public record DistrictResult(
    int Id,
    int ProvinceId,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website,
    string? HeadquarterEn,
    string? HeadquarterNe
)
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
            district.HeadquarterNe
        );
}
