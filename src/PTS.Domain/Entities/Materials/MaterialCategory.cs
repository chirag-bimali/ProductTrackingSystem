namespace PTS.Domain.Entities.Materials;

public class MaterialCategory
{
    public Guid Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Guid? ParentCategoryId { get; private set; }
    public MaterialCategory? ParentCategory { get; private set; }

    public ICollection<MaterialCategory> SubCategories { get; private set; } = new List<MaterialCategory>();
    public ICollection<Material> Materials { get; private set; } = new List<Material>();

    private MaterialCategory() { }

    public MaterialCategory(
        string nameEn,
        string nameNp,
        string? code = null,
        string? description = null,
        Guid? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));

        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        Code = code;
        Description = description;
        ParentCategoryId = parentCategoryId;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string nameEn,
        string nameNp,
        string? code = null,
        string? description = null,
        Guid? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));

        if (parentCategoryId.HasValue && parentCategoryId.Value == Id)
            throw new ArgumentException("Category cannot be its own parent.");

        NameEn = nameEn;
        NameNp = nameNp;
        Code = code;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
}
