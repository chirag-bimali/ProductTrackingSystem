using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class LocalAdminRepository(AppDbContext db)
    : Repository<LocalAdmin>(db),
        ILocalAdminRepository
{
    public async Task<(List<LocalAdmin> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct
    )
    {
        var query = Db.LocalAdmins
            .AsNoTracking()
            .Include(localAdmin => localAdmin.User)
            .Include(localAdmin => localAdmin.Municipality)
            .OrderBy(localAdmin => localAdmin.User.CreatedAt);

        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, totalCount);
    }
}
