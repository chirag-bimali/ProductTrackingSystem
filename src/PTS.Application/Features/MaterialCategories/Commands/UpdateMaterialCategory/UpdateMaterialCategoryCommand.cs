using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.MaterialCategories.Commands.UpdateMaterialCategory;

[RequirePermission(Permissions.MaterialCategories.Update)]
public sealed record UpdateMaterialCategoryCommand(
    Guid MaterialCategoryId,
    string NameEn,
    string NameNp,
    string? Code,
    string? Description,
    Guid? ParentCategoryId)
    : ICommand;
