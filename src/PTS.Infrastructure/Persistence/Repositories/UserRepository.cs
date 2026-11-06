using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext db)
        : base(db) { }

    public Task<bool> EmailExistsAsync(string email, CancellationToken ct = default) =>
        Db.Users.AnyAsync(u => u.Email == email.Trim().ToLowerInvariant(), ct);

    public Task<bool> ContactExistsAsync(string contact, CancellationToken ct = default) =>
        Db.Users.AnyAsync(u => u.Contact == contact.Trim(), ct);

    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return Db
            .Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email.Trim().ToLowerInvariant(), ct);
    }

    public Task<User?> GetByContactAsync(string contact, CancellationToken ct = default)
    {
        return Db
            .Users.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Contact == contact.Trim(), ct);
    }
}
