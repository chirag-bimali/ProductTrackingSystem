using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(AppDbContext db) => _db = db;

    public Task<Role?> GetByNameAsync(string nameEn, CancellationToken ct = default)
        => _db.Roles.FirstOrDefaultAsync(r => r.NameEn == nameEn, ct);
}