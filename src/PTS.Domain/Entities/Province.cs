namespace PTS.Domain.Entities;

public class Province
{
    public int Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string? AreaSqKm { get; private set; }
    public string? Website { get; private set; }
    public string? HeadquarterEn { get; private set; }
    public string? HeadquarterNe { get; private set; }

    public ICollection<District> Districts { get; private set; } = new List<District>();

    private Province() { }

    public Province(int id, string nameEn, string nameNp,
        string? areaSqKm = null, string? website = null, string? headquarterEn = null, string? headquarterNe = null)
    {
        Id = id;
        NameEn = nameEn;
        NameNp = nameNp;
        AreaSqKm = areaSqKm;
        Website = website;
        HeadquarterEn = headquarterEn;
        HeadquarterNe = headquarterNe;
    }
}