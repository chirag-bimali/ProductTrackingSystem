using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PTS.Application.Abstractions.Security;
using PTS.Domain.Entities;
using PTS.Domain.Entities.Identities;
using PTS.Infrastructure.Persistence;
using SE.Application.Abstractions.Security;

namespace PTS.Infrastructure.Security;

public sealed class PermissionService(AppDbContext db, IMemoryCache cache) : IPermissionService
{
    public async Task<bool> HasPermissionAsync(Guid userId, string permission, CancellationToken ct)
    {
        var permissions = await GetPermissionsAsync(userId, ct);
        return permissions.Contains(permission);
    }

    public async Task<IReadOnlyCollection<string>> GetPermissionsAsync(
        Guid userId,
        CancellationToken ct
    )
    {
        var permissions = await cache.GetOrCreateAsync(
            $"permissions:{userId}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);

                return await (
                    from ur in db.Set<UserRole>()
                    where ur.UserId == userId
                    join rp in db.Set<RolePermission>() on ur.RoleId equals rp.RoleId
                    join p in db.Set<Permission>() on rp.PermissionId equals p.Id
                    select p.Name
                )
                    .Distinct()
                    .OrderBy(name => name)
                    .ToListAsync(ct);
            }
        );

        return permissions ?? [];
    }
}
