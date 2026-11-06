using PTS.Application.Common.Security;
using PTS.Domain.Entities;

namespace PTS.Application.Features.Locations.Queries.GetAllProvinceQuery;

[RequirePermission(Permissions.No.Permission)]
public record GetAllProvinceQuery();
