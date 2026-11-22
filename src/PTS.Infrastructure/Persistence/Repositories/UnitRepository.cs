using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Repositories;

public sealed class UnitRepository(AppDbContext db) : Repository<Unit>(db), IUnitRepository
{
    public Task<List<Unit>> GetAllWithCategoryAsync(CancellationToken ct) =>
        Db.Units
            .AsNoTracking()
            .Include(unit => unit.UnitCategory)
            .OrderBy(unit => unit.NameEn)
            .ToListAsync(ct);

    public Task<Unit?> GetByIdWithCategoryAsync(Guid id, CancellationToken ct) =>
        Db.Units
            .AsNoTracking()
            .Include(unit => unit.UnitCategory)
            .FirstOrDefaultAsync(unit => unit.Id == id, ct);

    public Task<bool> UnitCategoryExistsAsync(Guid unitCategoryId, CancellationToken ct) =>
        Db.UnitCategories.AnyAsync(category => category.Id == unitCategoryId, ct);

    public Task<bool> IsAssignedToMaterialAsync(Guid unitId, CancellationToken ct) =>
        Db.MaterialUnits.AnyAsync(materialUnit => materialUnit.UnitId == unitId, ct);

    public Task<bool> HasConflictAsync(
        string nameEn,
        string abbreviation,
        Guid unitCategoryId,
        Guid? excludedUnitId,
        CancellationToken ct)
    {
        var normalizedNameEn = nameEn.ToLower();
        var normalizedAbbreviation = abbreviation.ToLower();

        return Db.Units.AnyAsync(
            unit =>
                unit.UnitCategoryId == unitCategoryId
                && (!excludedUnitId.HasValue || unit.Id != excludedUnitId.Value)
                && (unit.NameEn.ToLower() == normalizedNameEn
                    || unit.Abbreviation.ToLower() == normalizedAbbreviation),
            ct);
    }
}
