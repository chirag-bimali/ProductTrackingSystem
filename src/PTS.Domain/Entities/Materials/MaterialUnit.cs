namespace PTS.Domain.Entities.Materials;

public class MaterialUnit
{
    public Guid MaterialId { get; private set; }
    public Material Material { get; private set; } = null!;

    public Guid UnitId { get; private set; }
    public Unit Unit { get; private set; } = null!;

    /// <summary>
    /// The quantity in the material's base unit represented by one of this unit.
    /// For example, when kilogram is the base unit, one quintal has a factor of 100.
    /// </summary>
    public decimal ConversionFactorToBaseUnit { get; private set; }

    public bool IsBaseUnit { get; private set; }

    private MaterialUnit() { }

    public MaterialUnit(
        Guid materialId,
        Guid unitId,
        decimal conversionFactorToBaseUnit,
        bool isBaseUnit = false)
    {
        if (materialId == Guid.Empty)
            throw new ArgumentException("Material ID is required.", nameof(materialId));

        if (unitId == Guid.Empty)
            throw new ArgumentException("Unit ID is required.", nameof(unitId));

        ValidateConversionFactor(conversionFactorToBaseUnit, isBaseUnit);

        MaterialId = materialId;
        UnitId = unitId;
        ConversionFactorToBaseUnit = conversionFactorToBaseUnit;
        IsBaseUnit = isBaseUnit;
    }

    public decimal ConvertToBaseUnit(decimal quantity) =>
        quantity * ConversionFactorToBaseUnit;

    public void UpdateConversionFactor(decimal conversionFactorToBaseUnit)
    {
        ValidateConversionFactor(conversionFactorToBaseUnit, IsBaseUnit);
        ConversionFactorToBaseUnit = conversionFactorToBaseUnit;
    }

    internal void Reconfigure(decimal conversionFactorToBaseUnit, bool isBaseUnit)
    {
        ValidateConversionFactor(conversionFactorToBaseUnit, isBaseUnit);
        ConversionFactorToBaseUnit = conversionFactorToBaseUnit;
        IsBaseUnit = isBaseUnit;
    }

    private static void ValidateConversionFactor(
        decimal conversionFactorToBaseUnit,
        bool isBaseUnit)
    {
        if (conversionFactorToBaseUnit <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(conversionFactorToBaseUnit),
                "Conversion factor must be greater than zero.");
        }

        if (isBaseUnit && conversionFactorToBaseUnit != 1m)
        {
            throw new ArgumentException(
                "The base unit conversion factor must be 1.",
                nameof(conversionFactorToBaseUnit));
        }
    }
}
