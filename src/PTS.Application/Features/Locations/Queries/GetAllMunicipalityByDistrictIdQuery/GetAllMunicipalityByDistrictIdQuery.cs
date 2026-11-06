using PTS.Application.Common.Security;

namespace PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;

[RequirePermission(Permissions.No.Permission)]
public record GetAllMunicipalityByDistrictIdQuery(int DistrictId);
