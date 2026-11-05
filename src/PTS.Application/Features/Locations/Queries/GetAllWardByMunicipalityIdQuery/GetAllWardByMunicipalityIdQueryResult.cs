using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

public record WardResult(int Id, int MunicipalityId, int Number)
{
    public static WardResult FromEntity(Ward ward) =>
        new(ward.Id, ward.MunicipalityId, ward.Number);
}
