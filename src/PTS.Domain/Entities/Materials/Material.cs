namespace PTS.Domain.Entities.Materials;

public class Material
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsBatchTracked { get; private set; }
    public bool IsSerialTracked { get; private set; }
    public decimal MinimumStockLevel { get; private set; }
    public decimal ReorderLevel { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Guid MaterialCategoryId { get; private set; }
    public MaterialCategory MaterialCategory { get; private set; } = null!;

    public Guid BaseUnitId { get; private set; }
    public Unit BaseUnit { get; private set; } = null!;

    public ICollection<MaterialBatch> Batches { get; private set; } = new List<MaterialBatch>();
    public ICollection<MaterialSerialNumber> SerialNumbers { get; private set; } = new List<MaterialSerialNumber>();

    private Material() { }

    public Material(
        string code,
        string nameEn,
        string nameNp,
        Guid materialCategoryId,
        Guid baseUnitId,
        bool isBatchTracked = false,
        bool isSerialTracked = false,
        decimal minimumStockLevel = 0,
        decimal reorderLevel = 0,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Material code cannot be empty.", nameof(code));
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (materialCategoryId == Guid.Empty)
            throw new ArgumentException("Material category ID is required.", nameof(materialCategoryId));
        if (baseUnitId == Guid.Empty)
            throw new ArgumentException("Base unit ID is required.", nameof(baseUnitId));
        if (minimumStockLevel < 0)
            throw new ArgumentException("Minimum stock level cannot be negative.", nameof(minimumStockLevel));
        if (reorderLevel < 0)
            throw new ArgumentException("Reorder level cannot be negative.", nameof(reorderLevel));

        Id = Guid.NewGuid();
        Code = code;
        NameEn = nameEn;
        NameNp = nameNp;
        MaterialCategoryId = materialCategoryId;
        BaseUnitId = baseUnitId;
        IsBatchTracked = isBatchTracked;
        IsSerialTracked = isSerialTracked;
        MinimumStockLevel = minimumStockLevel;
        ReorderLevel = reorderLevel;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string code,
        string nameEn,
        string nameNp,
        Guid materialCategoryId,
        Guid baseUnitId,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Material code cannot be empty.", nameof(code));
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (materialCategoryId == Guid.Empty)
            throw new ArgumentException("Material category ID is required.", nameof(materialCategoryId));
        if (baseUnitId == Guid.Empty)
            throw new ArgumentException("Base unit ID is required.", nameof(baseUnitId));

        Code = code;
        NameEn = nameEn;
        NameNp = nameNp;
        MaterialCategoryId = materialCategoryId;
        BaseUnitId = baseUnitId;
        Description = description;
    }

    public void ConfigureTracking(bool isBatchTracked, bool isSerialTracked)
    {
        IsBatchTracked = isBatchTracked;
        IsSerialTracked = isSerialTracked;
    }

    public void SetStockLevels(decimal minimumStockLevel, decimal reorderLevel)
    {
        if (minimumStockLevel < 0)
            throw new ArgumentException("Minimum stock level cannot be negative.", nameof(minimumStockLevel));
        if (reorderLevel < 0)
            throw new ArgumentException("Reorder level cannot be negative.", nameof(reorderLevel));

        MinimumStockLevel = minimumStockLevel;
        ReorderLevel = reorderLevel;
    }
}
