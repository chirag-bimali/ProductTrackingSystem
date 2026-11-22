using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Repositories;

public sealed class MaterialCategoryRepository(AppDbContext db)
    : Repository<MaterialCategory>(db), IMaterialCategoryRepository
{
    public Task<List<MaterialCategory>> GetAllWithParentAsync(CancellationToken ct) =>
        Db.MaterialCategories
            .AsNoTracking()
            .Include(category => category.ParentCategory)
            .OrderBy(category => category.NameEn)
            .ToListAsync(ct);

    public Task<MaterialCategory?> GetByIdWithParentAsync(Guid id, CancellationToken ct) =>
        Db.MaterialCategories
            .AsNoTracking()
            .Include(category => category.ParentCategory)
            .FirstOrDefaultAsync(category => category.Id == id, ct);

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct) =>
        Db.MaterialCategories.AnyAsync(category => category.Id == id, ct);

    public Task<bool> HasConflictAsync(
        string nameEn,
        string? code,
        Guid? excludedCategoryId,
        CancellationToken ct)
    {
        var normalizedNameEn = nameEn.ToLower();
        var normalizedCode = code?.ToLower();

        return Db.MaterialCategories.AnyAsync(
            category =>
                (!excludedCategoryId.HasValue || category.Id != excludedCategoryId.Value)
                && (category.NameEn.ToLower() == normalizedNameEn
                    || (normalizedCode != null
                        && category.Code != null
                        && category.Code.ToLower() == normalizedCode)),
            ct);
    }

    public async Task<bool> WouldCreateCycleAsync(
        Guid categoryId,
        Guid proposedParentCategoryId,
        CancellationToken ct)
    {
        var parentLinks = await Db.MaterialCategories
            .AsNoTracking()
            .Select(category => new { category.Id, category.ParentCategoryId })
            .ToDictionaryAsync(category => category.Id, category => category.ParentCategoryId, ct);

        var visited = new HashSet<Guid>();
        Guid? currentId = proposedParentCategoryId;

        while (currentId.HasValue)
        {
            if (currentId.Value == categoryId || !visited.Add(currentId.Value))
                return true;

            currentId = parentLinks.GetValueOrDefault(currentId.Value);
        }

        return false;
    }

    public Task<bool> HasSubCategoriesAsync(Guid id, CancellationToken ct) =>
        Db.MaterialCategories.AnyAsync(category => category.ParentCategoryId == id, ct);

    public Task<bool> HasMaterialsAsync(Guid id, CancellationToken ct) =>
        Db.Materials.AnyAsync(material => material.MaterialCategoryId == id, ct);
}
