using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public sealed class UserRoleSeeder(AppDbContext db) : ISeeder
{
    public int Order => 6;

    private static readonly (string UserEmail, string RoleNameEn)[] UserRoles =
    [
        ("admin@admin.com", "SuperAdmin"),
        ("factory@admin.com", "FactoryAdmin"),
    ];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var userEmails = UserRoles.Select(ur => ur.UserEmail).ToList();
        var roleNames = UserRoles.Select(ur => ur.RoleNameEn).ToList();

        var users = await db
            .Users.Where(u => userEmails.Contains(u.Email))
            .ToDictionaryAsync(u => u.Email, ct);
        var roles = await db
            .Roles.Where(r => roleNames.Contains(r.NameEn))
            .ToDictionaryAsync(r => r.NameEn, ct);

        foreach (var (userEmail, roleNameEn) in UserRoles)
        {
            if (!users.TryGetValue(userEmail, out var user))
                throw new InvalidOperationException($"User '{userEmail}' not found.");

            if (!roles.TryGetValue(roleNameEn, out var role))
                throw new InvalidOperationException($"Role '{roleNameEn}' not found.");

            var userRoleExists = await db
                .Set<UserRole>()
                .AnyAsync(
                    ur => ur.UserId == user.Id && ur.RoleId == role.Id,
                    ct
                );

            if (!userRoleExists)
                db.Set<UserRole>().Add(new UserRole(user.Id, role.Id));
        }

        await db.SaveChangesAsync(ct);
    }
}