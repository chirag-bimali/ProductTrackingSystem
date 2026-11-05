using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface ILocationRepository
{
    Task<List<Province>> GetAllHierarchyAsync(CancellationToken ct);
    Task<List<Province>> GetAllProvincesAsync(CancellationToken ct);
    Task<List<District>> GetAllDistrictsByProvinceIdAsync(int provinceId, CancellationToken ct);

    Task<List<Municipality>> GetAllMunicipalitiesByDistrictIdAsync(
        int districtId,
        CancellationToken ct
    );

    Task<List<Ward>> GetAllWardsByMunicipalityIdAsync(int municipalityId, CancellationToken ct);
}
