using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.MaterialCategories.Commands.CreateMaterialCategory;

public sealed class CreateMaterialCategoryCommandHandler(
    IMaterialCategoryRepository materialCategoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMaterialCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateMaterialCategoryCommand request,
        CancellationToken cancellationToken)
    {
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
        }

        var nameEn = request.NameEn.Trim();
        var nameNp = request.NameNp.Trim();
        var code = NormalizeOptional(request.Code);
        var description = NormalizeOptional(request.Description);

        var hasConflict = await materialCategoryRepository.HasConflictAsync(
            nameEn,
            code,
            excludedCategoryId: null,
            cancellationToken);

        if (hasConflict)
        {
            return Error.Conflict(
                "material_categories.duplicate",
                "A material category with the same English name or code already exists.");
        }

        var category = new MaterialCategory(
            nameEn,
            nameNp,
            code,
            description,
            request.ParentCategoryId);

        await materialCategoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
