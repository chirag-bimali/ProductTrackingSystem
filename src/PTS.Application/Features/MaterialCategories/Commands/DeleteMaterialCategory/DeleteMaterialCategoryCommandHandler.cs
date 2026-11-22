using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.MaterialCategories.Commands.DeleteMaterialCategory;

public sealed class DeleteMaterialCategoryCommandHandler(
    IMaterialCategoryRepository materialCategoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMaterialCategoryCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        DeleteMaterialCategoryCommand request,
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

        var hasSubCategories = await materialCategoryRepository.HasSubCategoriesAsync(
            request.MaterialCategoryId,
            cancellationToken);

        if (hasSubCategories)
        {
            return Error.Conflict(
                "material_categories.has_children",
                "A material category with subcategories cannot be deleted.");
        }

        var hasMaterials = await materialCategoryRepository.HasMaterialsAsync(
            request.MaterialCategoryId,
            cancellationToken);

        if (hasMaterials)
        {
            return Error.Conflict(
                "material_categories.in_use",
                "A material category assigned to materials cannot be deleted.");
        }

        materialCategoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }
}
