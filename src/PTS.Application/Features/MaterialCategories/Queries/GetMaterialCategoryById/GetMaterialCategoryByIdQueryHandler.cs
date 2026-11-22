using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;

namespace PTS.Application.Features.MaterialCategories.Queries.GetMaterialCategoryById;

public sealed class GetMaterialCategoryByIdQueryHandler(
    IMaterialCategoryRepository materialCategoryRepository)
    : IQueryHandler<GetMaterialCategoryByIdQuery, MaterialCategoryResult>
{
    public async Task<Result<MaterialCategoryResult>> Handle(
        GetMaterialCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await materialCategoryRepository.GetByIdWithParentAsync(
            request.MaterialCategoryId,
            cancellationToken);

        if (category is null)
        {
            return Error.NotFound(
                "material_categories.not_found",
                "The material category was not found.");
        }

        return MaterialCategoryResult.FromEntity(category);
    }
}
