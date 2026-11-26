using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.Materials.Common;

namespace PTS.Application.Features.Materials.Commands.CreateMaterial;

[RequirePermission(Permissions.Materials.Create)]
public sealed record CreateMaterialCommand(
    string NameEn,
    string NameNp,
    Guid MaterialCategoryId,
    string? Description,
    IReadOnlyCollection<MaterialUnitInput> Units
) : ICommand<Guid>;
