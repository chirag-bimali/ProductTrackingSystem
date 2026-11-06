using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Abstractions.Security;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public class RoleSeeder(AppDbContext _db) : ISeeder
{
    public int Order => 5;
    public static readonly (string NameEn, string NameNp)[] Roles =
    [
        ("SuperAdmin", "सुपर एडमिन"),
        ("FactoryAdmin", "फ्याक्ट्री एडमिन"),
        ("Farmer", "किसान"),
        ("LocalAdmin", "स्थानीय एडमिन"),
        ("Collector", "संग्रहकर्ता"),
    ];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var seedRoleNameEns = Roles.Select(r => r.NameEn).ToList();
        var seedRoleNameNps = Roles.Select(r => r.NameNp).ToList();
        var existingRoles = await _db
            .Roles.Where(r => seedRoleNameEns.Contains(r.NameEn))
            .ToListAsync(ct);

        if (existingRoles.Any())
            return;

        // now add which roles aren't already in the database

        foreach (var (NameEn, NameNp) in Roles)
        {
            if (!existingRoles.Any(r => r.NameEn == NameEn && r.NameNp == NameNp))
            {
                var newRole = new Role(NameEn, NameNp);
                _db.Roles.Add(newRole);
            }
        }

        await _db.SaveChangesAsync(ct);
    }
}
