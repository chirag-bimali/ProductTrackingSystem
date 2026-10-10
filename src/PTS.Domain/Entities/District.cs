namespace PTS.Domain.Entities;

public class District
{
    public int Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string? AreaSqKm { get; private set; }
    public string? Website { get; private set; }
    public string? HeadquarterEn { get; private set; }
    public string? HeadquarterNe { get; private set; }

    public int ProvinceId { get; private set; }
    public Province Province { get; private set; } = null!;

    public ICollection<Municipality> Municipalities { get; private set; } = new List<Municipality>();

    private District() { }

    public District(int id, int provinceId, string nameEn, string nameNp,
        string? areaSqKm = null, string? website = null, string? headquarterEn = null, string? headquarterNe = null)
    {
        Id = id;
        ProvinceId = provinceId;
        NameEn = nameEn;
        NameNp = nameNp;
        AreaSqKm = areaSqKm;
        Website = website;
        HeadquarterEn = headquarterEn;
        HeadquarterNe = headquarterNe;
    }
}