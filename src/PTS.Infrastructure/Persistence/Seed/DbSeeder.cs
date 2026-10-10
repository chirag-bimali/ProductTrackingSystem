using System.Reflection;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Seed;

public class DbSeeder
{
    private readonly AppDbContext _db;

    public DbSeeder(AppDbContext db) => _db = db;

    public async Task SeedAsync(CancellationToken ct = default)
    {
        await SeedRolesAsync(ct);
        await SeedLocationsAsync(ct);
    }

    private async Task SeedRolesAsync(CancellationToken ct)
    {
        if (await _db.Roles.AnyAsync(ct)) return;

        _db.Roles.AddRange(
            new Role("SuperAdmin", "सुपर एडमिन"),
            new Role("FactoryAdmin", "फ्याक्ट्री एडमिन"),
            new Role("Farmer", "किसान"),
            new Role("LocalAdmin", "स्थानीय एडमिन"));

        await _db.SaveChangesAsync(ct);
    }

    private async Task SeedLocationsAsync(CancellationToken ct)
    {
        // Already seeded, so do nothing
        if (await _db.Provinces.AnyAsync(ct)) return;

        var data = ReadLocationData();

        await using var tx = await _db.Database.BeginTransactionAsync(ct);

        _db.Provinces.AddRange(data.Provinces.Select(p =>
            new Province(p.Id, p.NameEn, p.NameNp, p.AreaSqKm, p.Website, p.HeadquarterEn, p.HeadquarterNe)));

        _db.Districts.AddRange(data.Districts.Select(d =>
            new District(d.Id, d.ProvinceId, d.NameEn, d.NameNp, d.AreaSqKm, d.Website, d.HeadquarterEn, d.HeadquarterNe)));

        _db.Municipalities.AddRange(data.Municipalities.Select(m =>
            new Municipality(m.Id, m.DistrictId, m.CategoryId, m.NameEn, m.NameNp,
                areaSqKm: m.AreaSqKm, website: m.Website)));

        // The source has no ward IDs, so we number them 1..6743 in a fixed order
        var wardId = 1;
        foreach (var m in data.Municipalities.OrderBy(m => m.Id))
        {
            for (var number = 1; number <= m.WardCount; number++)
                _db.Wards.Add(new Ward(wardId++, m.Id, number));
        }

        await _db.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);
    }

    private static LocationData ReadLocationData()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
            .Single(n => n.EndsWith("locations.json", StringComparison.OrdinalIgnoreCase));

        using var stream = assembly.GetManifestResourceStream(resourceName)!;
        return JsonSerializer.Deserialize<LocationData>(stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    // Shapes that match locations.json
    private record LocationData(
        List<ProvinceRow> Provinces,
        List<DistrictRow> Districts,
        List<MunicipalityRow> Municipalities);

    private record ProvinceRow(int Id, string NameEn, string NameNp, string? AreaSqKm,
        string? Website, string? HeadquarterEn, string? HeadquarterNe);

    private record DistrictRow(int Id, int ProvinceId, string NameEn, string NameNp, string? AreaSqKm,
        string? Website, string? HeadquarterEn, string? HeadquarterNe);

    private record MunicipalityRow(int Id, int DistrictId, int CategoryId, string NameEn, string NameNp,
        string? AreaSqKm, string? Website, int WardCount);
}