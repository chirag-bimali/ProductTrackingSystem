using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.MaterialCategories.Commands.CreateMaterialCategory;

[RequirePermission(Permissions.MaterialCategories.Create)]
public sealed record CreateMaterialCategoryCommand(
    string NameEn,
    string NameNp,
    string? Code,
    string? Description,
    Guid? ParentCategoryId)
    : ICommand<Guid>;
