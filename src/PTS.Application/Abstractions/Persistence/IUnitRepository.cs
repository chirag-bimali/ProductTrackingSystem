using PTS.Domain.Entities.Materials;

namespace PTS.Application.Abstractions.Persistence;

public interface IUnitRepository : IRepository<Unit>
{
    Task<List<Unit>> GetAllWithCategoryAsync(CancellationToken ct);

    Task<Unit?> GetByIdWithCategoryAsync(Guid id, CancellationToken ct);

    Task<bool> UnitCategoryExistsAsync(Guid unitCategoryId, CancellationToken ct);

    Task<bool> IsAssignedToMaterialAsync(Guid unitId, CancellationToken ct);

    Task<bool> HasConflictAsync(
        string nameEn,
        string abbreviation,
        Guid unitCategoryId,
        Guid? excludedUnitId,
        CancellationToken ct);
}
