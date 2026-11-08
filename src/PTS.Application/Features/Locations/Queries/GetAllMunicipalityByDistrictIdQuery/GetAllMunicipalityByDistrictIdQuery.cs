using PTS.Application.Common.Security;

namespace PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;

using PTS.Application.Abstractions.Messaging;

[RequirePermission(Permissions.No.Permission)]
public record GetAllMunicipalityByDistrictIdQuery(int DistrictId)
    : IQuery<List<MunicipalityResult>>;
