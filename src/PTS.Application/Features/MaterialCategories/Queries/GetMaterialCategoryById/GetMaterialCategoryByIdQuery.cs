using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;
using PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;

namespace PTS.Application.Features.MaterialCategories.Queries.GetMaterialCategoryById;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetMaterialCategoryByIdQuery(Guid MaterialCategoryId)
    : IQuery<MaterialCategoryResult>;
