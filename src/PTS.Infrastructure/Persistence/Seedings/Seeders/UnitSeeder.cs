using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities.Materials;

namespace PTS.Infrastructure.Persistence.Seedings.Seeders;

public sealed class UnitSeeder(AppDbContext db) : ISeeder
{
    public int Order => 2;

    private static readonly (
        string NameEn,
        string NameNp,
        string Abbreviation,
        string CategoryNameEn)[] Units =
    [
        ("Kilogram", "किलोग्राम", "kg", "Weight"),
        ("Quintal", "क्विन्टल", "qtl", "Weight"),
        ("Gram", "ग्राम", "g", "Weight"),
        ("Litre", "लिटर", "L", "Volume"),
        ("Piece", "गोटा", "pcs", "Count"),
    ];

    public async Task SeedAsync(CancellationToken ct = default)
    {
        var requiredCategoryNames = Units
            .Select(unit => unit.CategoryNameEn)
            .Distinct()
            .ToList();

        var categories = await db
            .UnitCategories.Where(category => requiredCategoryNames.Contains(category.NameEn))
            .ToListAsync(ct);

        var categoriesByName = categories.ToDictionary(
            category => category.NameEn,
            StringComparer.OrdinalIgnoreCase);

        var missingCategoryNames = requiredCategoryNames
            .Where(categoryName => !categoriesByName.ContainsKey(categoryName))
            .ToList();

        if (missingCategoryNames.Count > 0)
        {
            throw new InvalidOperationException(
                $"Required unit categories were not found: {string.Join(", ", missingCategoryNames)}.");
        }

        var existingUnits = await db.Units.AsNoTracking().ToListAsync(ct);
        var missingUnits = new List<Unit>();

        foreach (var (NameEn, NameNp, Abbreviation, CategoryNameEn) in Units)
        {
            var category = categoriesByName[CategoryNameEn];
            var alreadyExists = existingUnits.Any(existing =>
                existing.UnitCategoryId == category.Id
                && (string.Equals(existing.NameEn, NameEn, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(
                        existing.Abbreviation,
                        Abbreviation,
                        StringComparison.OrdinalIgnoreCase)));

            if (!alreadyExists)
            {
                missingUnits.Add(
                    new Unit(NameEn, NameNp, Abbreviation, category.Id));
            }
        }

        if (missingUnits.Count == 0)
            return;

        await db.Units.AddRangeAsync(missingUnits, ct);
        await db.SaveChangesAsync(ct);
    }
}
