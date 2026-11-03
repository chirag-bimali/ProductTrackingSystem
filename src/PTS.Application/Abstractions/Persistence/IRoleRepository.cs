using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string nameEn, CancellationToken ct = default);
}
