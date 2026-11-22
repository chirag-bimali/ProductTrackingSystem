using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.Materials.Common;

namespace PTS.Application.Features.Materials.Commands.CreateMaterial;

[RequirePermission(Permissions.Materials.Create)]
public sealed record CreateMaterialCommand(
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
    : ICommand<Guid>;
