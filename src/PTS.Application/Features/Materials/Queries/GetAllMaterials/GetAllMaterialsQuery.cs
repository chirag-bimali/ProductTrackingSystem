using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.Materials.Queries.GetAllMaterials;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetAllMaterialsQuery() : IQuery<List<MaterialResult>>;
