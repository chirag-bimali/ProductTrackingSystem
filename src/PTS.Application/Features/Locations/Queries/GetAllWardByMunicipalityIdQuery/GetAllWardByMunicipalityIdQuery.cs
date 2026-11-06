using PTS.Application.Common.Security;

namespace PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

[RequirePermission(Permissions.No.Permission)]
public record GetAllWardByMunicipalityIdQuery(int MunicipalityId);
