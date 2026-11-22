using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.UnitCategories.Queries.GetAllUnitCategories;

public sealed record UnitCategoryResult(
    Guid Id,
    string NameEn,
    string NameNp,
    string? Description)
{
    public static UnitCategoryResult FromEntity(UnitCategory category) =>
        new(category.Id, category.NameEn, category.NameNp, category.Description);
}
