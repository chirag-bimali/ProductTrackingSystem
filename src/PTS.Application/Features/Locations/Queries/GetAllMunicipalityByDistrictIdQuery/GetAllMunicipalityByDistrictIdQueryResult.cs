using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;

public record MunicipalityResult(
    int Id,
    int DistrictId,
    int CategoryId,
    string NameEn,
    string NameNp,
    string? AreaSqKm,
    string? Website
)
{
    public static MunicipalityResult FromEntity(Municipality municipality) =>
        new(
            municipality.Id,
            municipality.DistrictId,
            municipality.CategoryId,
            municipality.NameEn,
            municipality.NameNp,
            municipality.AreaSqKm,
            municipality.Website
        );
}
