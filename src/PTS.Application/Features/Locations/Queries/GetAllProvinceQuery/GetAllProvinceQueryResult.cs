using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllProvincesQuery;

public record ProvinceResult(
    int Id,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website,
    string? HeadquarterEn,
    string? HeadquarterNe
)
{
    public static ProvinceResult FromEntity(Province province) =>
        new(
            province.Id,
            province.NameEn,
            province.NameNp,
            province.AreaSqKm,
            province.Website,
            province.HeadquarterEn,
            province.HeadquarterNe
        );
}
