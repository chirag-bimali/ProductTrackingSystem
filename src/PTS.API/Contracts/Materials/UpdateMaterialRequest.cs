using PTS.Application.Features.Materials.Common;

namespace PTS.API.Contracts.Materials;

public sealed record UpdateMaterialRequest(
    string Code,
    string NameEn,
    string NameNp,
    Guid MaterialCategoryId,
    bool IsBatchTracked,
    bool IsSerialTracked,
    decimal MinimumStockLevel,
    decimal ReorderLevel,
    string? Description,
    IReadOnlyCollection<MaterialUnitInput> Units);
