namespace PTS.API.Contracts.Units;

public sealed record UpdateUnitRequest(
    string NameEn,
    string NameNp,
    string Abbreviation,
    Guid UnitCategoryId);
