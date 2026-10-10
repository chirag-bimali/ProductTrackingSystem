using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string nameEn, CancellationToken ct = default);
}