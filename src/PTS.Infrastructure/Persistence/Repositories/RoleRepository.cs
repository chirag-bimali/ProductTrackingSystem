using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext db)
        : base(db) { }

    public Task<Role?> GetByNameAsync(string nameEn, CancellationToken ct = default) =>
        Db.Roles.FirstOrDefaultAsync(r => r.NameEn == nameEn, ct);
}
