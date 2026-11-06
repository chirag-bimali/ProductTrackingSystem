using PTS.Application.Common.Security;

namespace PTS.Application.Features.Users.Queries.GetAllLocalAdmin;

[RequirePermission(Permissions.LocalAdmin.ReadAll)]
public record GetAllLocalAdminQuery(int PageNumber = 1, int PageSize = 10);
