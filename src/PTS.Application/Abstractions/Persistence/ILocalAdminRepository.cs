using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface ILocalAdminRepository : IRepository<LocalAdmin>
{
    Task<(List<LocalAdmin> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct
    );
}
