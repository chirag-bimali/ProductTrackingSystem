using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Security;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public sealed class PermissionSeeder(AppDbContext db) : ISeeder
{
    public int Order => 3;

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var existing = await db.Set<Permission>().ToListAsync(ct);

        foreach (var name in Permissions.All.Where(n => existing.All(e => e.Name != n)))
        {
            var permission = new Permission(name);
            db.Add(permission);
            existing.Add(permission);
        }
        await db.SaveChangesAsync(ct);

    }
}
