namespace PTS.Domain.Entities;

public class Municipality
{
    public int Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public int CategoryId { get; private set; }
    public string? AreaSqKm { get; private set; }
    public string? Website { get; private set; }

    // Ward numbers, e.g. ["1", "2", "3"]. Npgsql stores this as a PostgreSQL text[].
    public ICollection<Ward> Wards { get; private set; } = new List<Ward>();

    public int DistrictId { get; private set; }
    public District District { get; private set; } = null!;

    private Municipality() { }

    public Municipality(int id, int districtId, int categoryId, string nameEn, string nameNp,
        List<Ward>? wards = null, string? areaSqKm = null, string? website = null)
    {
        Id = id;
        DistrictId = districtId;
        CategoryId = categoryId;
        NameEn = nameEn;
        NameNp = nameNp;
        Wards = wards ?? new List<Ward>();
        AreaSqKm = areaSqKm;
        Website = website;
    }
}