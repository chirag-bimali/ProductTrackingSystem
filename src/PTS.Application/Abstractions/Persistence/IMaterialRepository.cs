using PTS.Domain.Entities.Materials;

namespace PTS.Application.Abstractions.Persistence;

public interface IMaterialRepository : IRepository<Material>
{
    Task<List<Material>> GetAllWithDetailsAsync(CancellationToken ct);

    Task<Material?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct);

    Task<Material?> GetByIdForUpdateAsync(Guid id, CancellationToken ct);

    Task<bool> MaterialCategoryExistsAsync(Guid materialCategoryId, CancellationToken ct);

    Task<bool> UnitsExistAsync(IReadOnlyCollection<Guid> unitIds, CancellationToken ct);

    Task<bool> HasConflictAsync(
        string code,
        string nameEn,
        Guid? excludedMaterialId,
        CancellationToken ct);
}
