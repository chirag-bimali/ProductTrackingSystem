using PTS.Domain.Entities.Materials;

namespace PTS.Application.Abstractions.Persistence;

public interface IMaterialCategoryRepository : IRepository<MaterialCategory>
{
    Task<List<MaterialCategory>> GetAllWithParentAsync(CancellationToken ct);

    Task<MaterialCategory?> GetByIdWithParentAsync(Guid id, CancellationToken ct);

    Task<bool> ExistsAsync(Guid id, CancellationToken ct);

    Task<bool> HasConflictAsync(
        string nameEn,
        string? code,
        Guid? excludedCategoryId,
        CancellationToken ct);

    Task<bool> WouldCreateCycleAsync(
        Guid categoryId,
        Guid proposedParentCategoryId,
        CancellationToken ct);

    Task<bool> HasSubCategoriesAsync(Guid id, CancellationToken ct);

    Task<bool> HasMaterialsAsync(Guid id, CancellationToken ct);
}
