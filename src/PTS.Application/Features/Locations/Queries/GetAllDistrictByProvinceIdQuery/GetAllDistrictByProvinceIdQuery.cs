using PTS.Application.Common.Security;

namespace PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;

[RequirePermission(Permissions.No.Permission)]
public record GetAllDistrictByProvinceIdQuery(int ProvinceId);
