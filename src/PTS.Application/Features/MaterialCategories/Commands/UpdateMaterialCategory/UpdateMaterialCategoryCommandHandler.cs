using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.MaterialCategories.Commands.UpdateMaterialCategory;

public sealed class UpdateMaterialCategoryCommandHandler(
    IMaterialCategoryRepository materialCategoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMaterialCategoryCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        UpdateMaterialCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await materialCategoryRepository.GetByIdAsync(
            request.MaterialCategoryId,
            cancellationToken);

        if (category is null)
        {
            return Error.NotFound(
                "material_categories.not_found",
                "The material category was not found.");
        }

        if (request.ParentCategoryId == request.MaterialCategoryId)
        {
            return Error.Validation(
                "material_categories.invalid_parent",
                "A material category cannot be its own parent.");
        }

        if (request.ParentCategoryId.HasValue)
        {
            var parentExists = await materialCategoryRepository.ExistsAsync(
                request.ParentCategoryId.Value,
                cancellationToken);

            if (!parentExists)
            {
                return Error.NotFound(
                    "material_categories.parent_not_found",
                    "The selected parent material category was not found.");
            }

            var createsCycle = await materialCategoryRepository.WouldCreateCycleAsync(
                request.MaterialCategoryId,
                request.ParentCategoryId.Value,
                cancellationToken);

            if (createsCycle)
            {
                return Error.Validation(
                    "material_categories.parent_cycle",
                    "The selected parent would create a category hierarchy cycle.");
            }
        }

        var nameEn = request.NameEn.Trim();
        var nameNp = request.NameNp.Trim();
        var code = NormalizeOptional(request.Code);
        var description = NormalizeOptional(request.Description);

        var hasConflict = await materialCategoryRepository.HasConflictAsync(
            nameEn,
            code,
            request.MaterialCategoryId,
            cancellationToken);

        if (hasConflict)
        {
            return Error.Conflict(
                "material_categories.duplicate",
                "A material category with the same English name or code already exists.");
        }

        category.Update(nameEn, nameNp, code, description, request.ParentCategoryId);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
