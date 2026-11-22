using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Security;

namespace PTS.Application.Features.MaterialCategories.Commands.DeleteMaterialCategory;

[RequirePermission(Permissions.MaterialCategories.Delete)]
public sealed record DeleteMaterialCategoryCommand(Guid MaterialCategoryId) : ICommand;
