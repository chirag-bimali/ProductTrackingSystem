using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;

public sealed record MaterialCategoryResult(
    Guid Id,
    string NameEn,
    string NameNp,
    string? Code,
    string? Description,
    Guid? ParentCategoryId,
    string? ParentCategoryNameEn,
    string? ParentCategoryNameNp,
    DateTime CreatedAt)
{
    public static MaterialCategoryResult FromEntity(MaterialCategory category) =>
        new(
            category.Id,
            category.NameEn,
            category.NameNp,
            category.Code,
            category.Description,
            category.ParentCategoryId,
            category.ParentCategory?.NameEn,
            category.ParentCategory?.NameNp,
            category.CreatedAt);
}
