using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task<User?> GetByIdentifierAsync(string identifier, CancellationToken ct = default)
    {
        var value = identifier.Trim();

        // An "@" means it is an email, otherwise we treat it as a contact number.
        // Emails are always stored in lowercase.
        return value.Contains('@')
            ? await _db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == value.ToLowerInvariant(), ct)
            : await _db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Contact == value, ct);
    }

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        => _db.Users.AnyAsync(u => u.Email == email.Trim().ToLowerInvariant(), ct);

    public Task<bool> ContactExistsAsync(string contact, CancellationToken ct = default)
        => _db.Users.AnyAsync(u => u.Contact == contact.Trim(), ct);

    public async Task AddAsync(User user, CancellationToken ct = default)
        => await _db.Users.AddAsync(user, ct);
}