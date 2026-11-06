namespace PTS.Domain.Entities.Materials;

public class UnitCategory
{
    public Guid Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<Unit> Units { get; private set; } = new List<Unit>();

    private UnitCategory() { }

    public UnitCategory(string nameEn, string nameNp, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));

        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string nameEn, string nameNp, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));

        NameEn = nameEn;
        NameNp = nameNp;
        Description = description;
    }
}
