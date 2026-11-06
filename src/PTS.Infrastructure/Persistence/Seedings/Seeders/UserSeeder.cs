using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Abstractions.Security;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public class UserSeeder(AppDbContext _db, IPasswordHasher passwordHasher) : ISeeder
{
    public int Order => 5;
    public static readonly (
        string NameEn,
        string NameNp,
        string Contact,
        string Email,
        string PasswordHash,
        string? ImageUrl
    )[] Users =
    [
        ("Super Admin", "सुपर एडमिन", "9865230147", "admin@admin.com", "Admin@1234", null),
        (
            "Factory Admin",
            "फ्याक्ट्री एडमिन",
            "9876541230",
            "factory@admin.com",
            "Admin@1234",
            null
        ),
    ];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var seedUserEmails = Users.Select(u => u.Email).ToList();
        // Seed users
        var existingUsers = await _db
            .Users.Where(u => seedUserEmails.Contains(u.Email))
            .ToListAsync(ct);

        foreach (var (NameEn, NameNp, Contact, Email, PasswordHash, ImageUrl) in Users)
        {
            if (!existingUsers.Any(u => u.Email == Email))
            {
                var newUser = new User(
                    NameEn,
                    NameNp,
                    Contact,
                    Email,
                    passwordHasher.Hash(PasswordHash),
                    ImageUrl
                );
                _db.Users.Add(newUser);
            }
        }
        await _db.SaveChangesAsync(ct);
    }
}
