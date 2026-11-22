using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Materials.Commands.DeleteMaterial;

[RequirePermission(Permissions.Materials.Delete)]
public sealed record DeleteMaterialCommand(Guid MaterialId) : ICommand;
