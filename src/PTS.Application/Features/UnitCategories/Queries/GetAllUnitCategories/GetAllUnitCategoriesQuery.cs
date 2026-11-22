using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.UnitCategories.Queries.GetAllUnitCategories;

[RequirePermission(Permissions.No.Permission)]
public sealed record GetAllUnitCategoriesQuery() : IQuery<List<UnitCategoryResult>>;
