using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Repositories;

public sealed class MaterialRepository(AppDbContext db)
    : Repository<Material>(db), IMaterialRepository
{
    public Task<List<Material>> GetAllWithDetailsAsync(CancellationToken ct) =>
        DetailsQuery(asNoTracking: true)
            .OrderBy(material => material.NameEn)
            .ToListAsync(ct);

    public Task<Material?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct) =>
        DetailsQuery(asNoTracking: true)
            .FirstOrDefaultAsync(material => material.Id == id, ct);

    public Task<Material?> GetByIdForUpdateAsync(Guid id, CancellationToken ct) =>
        Db.Materials
            .Include(material => material.MaterialUnits)
            .FirstOrDefaultAsync(material => material.Id == id, ct);

    public Task<bool> MaterialCategoryExistsAsync(
        Guid materialCategoryId,
        CancellationToken ct) =>
        Db.MaterialCategories.AnyAsync(category => category.Id == materialCategoryId, ct);

    public async Task<bool> UnitsExistAsync(
        IReadOnlyCollection<Guid> unitIds,
        CancellationToken ct)
    {
        var distinctUnitIds = unitIds.Distinct().ToList();
        var existingCount = await Db.Units.CountAsync(
            unit => distinctUnitIds.Contains(unit.Id),
            ct);

        return existingCount == distinctUnitIds.Count;
    }

    public Task<bool> HasConflictAsync(
        string code,
        string nameEn,
        Guid? excludedMaterialId,
        CancellationToken ct)
    {
        var normalizedCode = code.ToLower();
        var normalizedNameEn = nameEn.ToLower();

        return Db.Materials.AnyAsync(
            material =>
                (!excludedMaterialId.HasValue || material.Id != excludedMaterialId.Value)
                && (material.Code.ToLower() == normalizedCode
                    || material.NameEn.ToLower() == normalizedNameEn),
            ct);
    }

    private IQueryable<Material> DetailsQuery(bool asNoTracking)
    {
        IQueryable<Material> query = Db.Materials;

        if (asNoTracking)
            query = query.AsNoTracking();

        return query
            .Include(material => material.MaterialCategory)
            .Include(material => material.MaterialUnits)
                .ThenInclude(materialUnit => materialUnit.Unit)
                    .ThenInclude(unit => unit.UnitCategory)
            .AsSplitQuery();
    }
}
