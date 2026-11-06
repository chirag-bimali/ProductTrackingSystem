using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Security;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public sealed class RolePermissionSeeder(AppDbContext db) : ISeeder
{
    public int Order => 4;

    private static readonly string[] superAdminPermissions = [.. Permissions.All];

    private static readonly string[] farmerPermissions = [Permissions.Farmer.Access];

    private static readonly string[] localAdminPermissions = [Permissions.LocalAdmin.Access];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        await db.Set<RolePermission>().ExecuteDeleteAsync(ct);
        // get all permissions
        var permissions = await db.Set<Permission>().ToListAsync(ct);
        var superAdminRole = await db.Set<Role>()
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.NameEn == "SuperAdmin", ct);
        var localAdminRole = await db.Set<Role>()
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.NameEn == "LocalAdmin", ct);
        var farmer = await db.Set<Role>()
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.NameEn == "Farmer", ct);

        if (superAdminRole is null)
            throw new InvalidOperationException("Super Admin role not found.");
        if (localAdminRole is null)
            throw new InvalidOperationException("Local Admin role not found.");
        if (farmer is null)
            throw new InvalidOperationException("Farmer role not found.");

        for (int i = 0; i < superAdminPermissions.Length; i++)
        {
            var permission = permissions.FirstOrDefault(p => p.Name == superAdminPermissions[i]);
            if (permission is not null)
            {
                if (
                    permission.Name == Permissions.Farmer.Access
                    || permission.Name == Permissions.LocalAdmin.Access
                )
                    continue;

                if (!superAdminRole.RolePermissions.Any(rp => rp.PermissionId == permission.Id))
                {
                    superAdminRole.RolePermissions.Add(
                        new RolePermission(superAdminRole.Id, permission.Id)
                    );
                }
            }
        }

        for (int i = 0; i < localAdminPermissions.Length; i++)
        {
            var permission = permissions.FirstOrDefault(p => p.Name == localAdminPermissions[i]);
            if (permission is not null)
            {
                if (!localAdminRole.RolePermissions.Any(rp => rp.PermissionId == permission.Id))
                {
                    localAdminRole.RolePermissions.Add(
                        new RolePermission(localAdminRole.Id, permission.Id)
                    );
                }
            }
        }

        for (int i = 0; i < farmerPermissions.Length; i++)
        {
            var permission = permissions.FirstOrDefault(p => p.Name == farmerPermissions[i]);
            if (permission is not null)
            {
                if (!farmer.RolePermissions.Any(rp => rp.PermissionId == permission.Id))
                {
                    farmer.RolePermissions.Add(new RolePermission(farmer.Id, permission.Id));
                }
            }
        }

        await db.SaveChangesAsync(ct);
    }
}
