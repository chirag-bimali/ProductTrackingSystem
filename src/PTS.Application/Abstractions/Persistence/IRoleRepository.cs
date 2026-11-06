using PTS.Domain.Entities.Identities;

namespace PTS.Application.Abstractions.Persistence;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string nameEn, CancellationToken ct = default);
}
