using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.Materials.Queries.GetAllMaterials;

public sealed record MaterialUnitResult(
    Guid UnitId,
    string NameEn,
    string NameNp,
    string Abbreviation,
    Guid UnitCategoryId,
    string UnitCategoryNameEn,
    string UnitCategoryNameNp,
    decimal ConversionFactorToBaseUnit,
    bool IsBaseUnit)
{
    public static MaterialUnitResult FromEntity(MaterialUnit materialUnit) =>
        new(
            materialUnit.UnitId,
            materialUnit.Unit.NameEn,
            materialUnit.Unit.NameNp,
            materialUnit.Unit.Abbreviation,
            materialUnit.Unit.UnitCategoryId,
            materialUnit.Unit.UnitCategory.NameEn,
            materialUnit.Unit.UnitCategory.NameNp,
            materialUnit.ConversionFactorToBaseUnit,
            materialUnit.IsBaseUnit);
}

public sealed record MaterialResult(
    Guid Id,
    string Code,
    string NameEn,
    string NameNp,
    string? Description,
    bool IsBatchTracked,
    bool IsSerialTracked,
    decimal MinimumStockLevel,
    decimal ReorderLevel,
    Guid MaterialCategoryId,
    string MaterialCategoryNameEn,
    string MaterialCategoryNameNp,
    DateTime CreatedAt,
    List<MaterialUnitResult> Units)
{
    public static MaterialResult FromEntity(Material material) =>
        new(
            material.Id,
            material.Code,
            material.NameEn,
            material.NameNp,
            material.Description,
            material.IsBatchTracked,
            material.IsSerialTracked,
            material.MinimumStockLevel,
            material.ReorderLevel,
            material.MaterialCategoryId,
            material.MaterialCategory.NameEn,
            material.MaterialCategory.NameNp,
            material.CreatedAt,
            material.MaterialUnits
                .OrderByDescending(materialUnit => materialUnit.IsBaseUnit)
                .ThenBy(materialUnit => materialUnit.Unit.NameEn)
                .Select(MaterialUnitResult.FromEntity)
                .ToList());
}
