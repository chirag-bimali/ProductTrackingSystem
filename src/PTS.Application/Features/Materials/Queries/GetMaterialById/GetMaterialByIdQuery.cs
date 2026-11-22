using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.Materials.Queries.GetAllMaterials;

namespace PTS.Application.Features.Materials.Queries.GetMaterialById;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetMaterialByIdQuery(Guid MaterialId) : IQuery<MaterialResult>;
