using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public sealed class UnitCategorySeeder(AppDbContext db) : ISeeder
{
    public int Order => 1;

    private static readonly (string NameEn, string NameNp)[] Categories =
    [
        ("Weight", "तौल"),
        ("Volume", "आयतन"),
        ("Count", "संख्या"),
    ];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var categoryNames = Categories.Select(category => category.NameEn).ToList();

        var existingNames = await db
            .UnitCategories.AsNoTracking()
            .Where(category => categoryNames.Contains(category.NameEn))
            .Select(category => category.NameEn)
            .ToListAsync(ct);

        var existingNameSet = existingNames.ToHashSet(StringComparer.OrdinalIgnoreCase);

        var missingCategories = Categories
            .Where(category => !existingNameSet.Contains(category.NameEn))
            .Select(category => new UnitCategory(category.NameEn, category.NameNp))
            .ToList();

        if (missingCategories.Count == 0)
            return;

        await db.UnitCategories.AddRangeAsync(missingCategories, ct);
        await db.SaveChangesAsync(ct);
    }
}
