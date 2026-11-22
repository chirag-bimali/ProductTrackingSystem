using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetAllMaterialCategoriesQuery()
    : IQuery<List<MaterialCategoryResult>>;
