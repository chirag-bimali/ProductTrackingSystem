using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.Units.Queries.GetAllUnits;

public sealed record UnitResult(
    Guid Id,
    string NameEn,
    string NameNp,
    string Abbreviation,
    Guid UnitCategoryId,
    string UnitCategoryNameEn,
    string UnitCategoryNameNp)
{
    public static UnitResult FromEntity(Unit unit) =>
        new(
            unit.Id,
            unit.NameEn,
            unit.NameNp,
            unit.Abbreviation,
            unit.UnitCategoryId,
            unit.UnitCategory.NameEn,
            unit.UnitCategory.NameNp);
}
