namespace PTS.API.Contracts.MaterialCategories;

public sealed record UpdateMaterialCategoryRequest(
    string NameEn,
    string NameNp,
    string? Code,
    string? Description,
    Guid? ParentCategoryId);
