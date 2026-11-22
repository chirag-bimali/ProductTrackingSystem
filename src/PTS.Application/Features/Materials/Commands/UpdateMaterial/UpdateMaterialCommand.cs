using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.Materials.Common;

namespace PTS.Application.Features.Materials.Commands.UpdateMaterial;

[RequirePermission(Permissions.Materials.Update)]
public sealed record UpdateMaterialCommand(
    Guid MaterialId,
    string Code,
    string NameEn,
    string NameNp,
    Guid MaterialCategoryId,
    bool IsBatchTracked,
    bool IsSerialTracked,
    decimal MinimumStockLevel,
    decimal ReorderLevel,
    string? Description,
    IReadOnlyCollection<MaterialUnitInput> Units)
    : ICommand;
