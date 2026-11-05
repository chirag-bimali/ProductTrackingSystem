using Microsoft.EntityFrameworkCore;
using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Persistence.Repositories;

public sealed class LocationRepository(AppDbContext db) : ILocationRepository
{
    public Task<List<District>> GetAllDistrictsByProvinceIdAsync(
        int provinceId,
        CancellationToken ct
    )
    {
        return db
            .Districts.AsNoTracking()
            .Where(district => district.ProvinceId == provinceId)
            .OrderBy(district => district.Id)
            .ToListAsync(ct);
    }

    public Task<List<Province>> GetAllHierarchyAsync(CancellationToken ct) =>
        db
            .Provinces.AsNoTracking()
            .Include(province => province.Districts)
                .ThenInclude(district => district.Municipalities)
                    .ThenInclude(municipality => municipality.Wards)
            .OrderBy(province => province.Id)
            .ToListAsync(ct);

    public Task<List<Municipality>> GetAllMunicipalitiesByDistrictIdAsync(
        int districtId,
        CancellationToken ct
    )
    {
        return db
            .Municipalities.AsNoTracking()
            .Where(municipality => municipality.DistrictId == districtId)
            .OrderBy(municipality => municipality.Id)
            .ToListAsync(ct);
    }

    public Task<List<Province>> GetAllProvincesAsync(CancellationToken ct)
    {
        return db.Provinces.AsNoTracking().OrderBy(province => province.Id).ToListAsync(ct);
    }

    public Task<List<Ward>> GetAllWardsByMunicipalityIdAsync(
        int municipalityId,
        CancellationToken ct
    )
    {
        return db
            .Wards.AsNoTracking()
            .Where(ward => ward.MunicipalityId == municipalityId)
            .OrderBy(ward => ward.Id)
            .ToListAsync(ct);
    }
}
