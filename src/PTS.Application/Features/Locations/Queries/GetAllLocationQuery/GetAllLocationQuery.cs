using PTS.Application.Common.Security;

namespace PTS.Application.Features.Locations.Queries.GetAllLocationQuery;

[RequirePermission(Permissions.No.Permission)]
public record GetAllLocationQuery();
