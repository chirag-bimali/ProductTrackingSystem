namespace PTS.Domain.Entities.Materials;

public class Material
{
    public Guid Id { get; private set; }
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

    public ICollection<MaterialUnit> MaterialUnits { get; private set; } = new List<MaterialUnit>();
    public ICollection<MaterialBatch> Batches { get; private set; } = new List<MaterialBatch>();
    public ICollection<MaterialSerialNumber> SerialNumbers { get; private set; } =
        new List<MaterialSerialNumber>();

    private Material() { }

    public Material(
        string nameEn,
        string nameNp,
        Guid materialCategoryId,
        IEnumerable<(Guid UnitId, decimal ConversionFactorToBaseUnit, bool IsBaseUnit)> units,
        string? description = null
    )
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (materialCategoryId == Guid.Empty)
            throw new ArgumentException(
                "Material category ID is required.",
                nameof(materialCategoryId)
            );
        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        MaterialCategoryId = materialCategoryId;
        Description = description;
        CreatedAt = DateTime.UtcNow;

        ReplaceUnits(units);
    }

    public void Update(
        string nameEn,
        string nameNp,
        Guid materialCategoryId,
        string? description = null
    )
    {
        if (string.IsNullOrWhiteSpace(nameEn))
            throw new ArgumentException("English name cannot be empty.", nameof(nameEn));
        if (string.IsNullOrWhiteSpace(nameNp))
            throw new ArgumentException("Nepali name cannot be empty.", nameof(nameNp));
        if (materialCategoryId == Guid.Empty)
            throw new ArgumentException(
                "Material category ID is required.",
                nameof(materialCategoryId)
            );

        NameEn = nameEn;
        NameNp = nameNp;
        MaterialCategoryId = materialCategoryId;
        Description = description;
    }

    public void ReplaceUnits(
        IEnumerable<(Guid UnitId, decimal ConversionFactorToBaseUnit, bool IsBaseUnit)> units
    )
    {
        ArgumentNullException.ThrowIfNull(units);

        var requestedUnits = units.ToList();

        if (requestedUnits.Count == 0)
            throw new ArgumentException("At least one material unit is required.", nameof(units));

        if (requestedUnits.Any(unit => unit.UnitId == Guid.Empty))
            throw new ArgumentException("Unit IDs cannot be empty.", nameof(units));

        if (requestedUnits.Select(unit => unit.UnitId).Distinct().Count() != requestedUnits.Count)
            throw new ArgumentException("A unit cannot be assigned more than once.", nameof(units));

        if (requestedUnits.Count(unit => unit.IsBaseUnit) != 1)
            throw new ArgumentException("Exactly one base unit is required.", nameof(units));

        var requestedBaseUnitId = requestedUnits.Single(unit => unit.IsBaseUnit).UnitId;
        var existingBaseUnit = MaterialUnits.SingleOrDefault(unit => unit.IsBaseUnit);

        if (existingBaseUnit is not null && existingBaseUnit.UnitId != requestedBaseUnitId)
        {
            throw new InvalidOperationException(
                "The base unit cannot be changed after the material is created."
            );
        }

        foreach (var unit in requestedUnits)
        {
            if (unit.ConversionFactorToBaseUnit <= 0)
                throw new ArgumentException(
                    "Conversion factors must be greater than zero.",
                    nameof(units)
                );

            if (unit.IsBaseUnit && unit.ConversionFactorToBaseUnit != 1m)
                throw new ArgumentException(
                    "The base unit conversion factor must be 1.",
                    nameof(units)
                );
        }

        var requestedUnitIds = requestedUnits.Select(unit => unit.UnitId).ToHashSet();
        var removedUnits = MaterialUnits
            .Where(unit => !requestedUnitIds.Contains(unit.UnitId))
            .ToList();

        foreach (var removedUnit in removedUnits)
            MaterialUnits.Remove(removedUnit);

        foreach (var requestedUnit in requestedUnits)
        {
            var existingUnit = MaterialUnits.FirstOrDefault(unit =>
                unit.UnitId == requestedUnit.UnitId
            );

            if (existingUnit is null)
            {
                MaterialUnits.Add(
                    new MaterialUnit(
                        Id,
                        requestedUnit.UnitId,
                        requestedUnit.ConversionFactorToBaseUnit,
                        requestedUnit.IsBaseUnit
                    )
                );
            }
            else
            {
                existingUnit.Reconfigure(
                    requestedUnit.ConversionFactorToBaseUnit,
                    requestedUnit.IsBaseUnit
                );
            }
        }
    }
}
