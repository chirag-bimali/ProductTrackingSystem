namespace PTS.Domain.Entities.Materials;

public class Unit
{
    public Guid Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string Abbreviation { get; private set; } = string.Empty;
    public bool IsBaseUnit { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Guid UnitCategoryId { get; private set; }
    public UnitCategory UnitCategory { get; private set; } = null!;

    public ICollection<UnitConversion> FromConversions { get; private set; } = new List<UnitConversion>();
    public ICollection<UnitConversion> ToConversions { get; private set; } = new List<UnitConversion>();
    public ICollection<Material> Materials { get; private set; } = new List<Material>();

    private Unit() { }

    public Unit(
        string nameEn,
        string nameNp,
        string abbreviation,
        Guid unitCategoryId,
        bool isBaseUnit = false)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (string.IsNullOrWhiteSpace(abbreviation))
            throw new ArgumentException("Abbreviation cannot be empty.", nameof(abbreviation));
        if (unitCategoryId == Guid.Empty)
            throw new ArgumentException("Unit category ID is required.", nameof(unitCategoryId));

        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        Abbreviation = abbreviation;
        UnitCategoryId = unitCategoryId;
        IsBaseUnit = isBaseUnit;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string nameEn, string nameNp, string abbreviation, Guid unitCategoryId, bool isBaseUnit)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (string.IsNullOrWhiteSpace(abbreviation))
            throw new ArgumentException("Abbreviation cannot be empty.", nameof(abbreviation));
        if (unitCategoryId == Guid.Empty)
            throw new ArgumentException("Unit category ID is required.", nameof(unitCategoryId));

        NameEn = nameEn;
        NameNp = nameNp;
        Abbreviation = abbreviation;
        UnitCategoryId = unitCategoryId;
        IsBaseUnit = isBaseUnit;
    }

    public void SetBaseUnit(bool isBaseUnit)
    {
        IsBaseUnit = isBaseUnit;
    }
}
