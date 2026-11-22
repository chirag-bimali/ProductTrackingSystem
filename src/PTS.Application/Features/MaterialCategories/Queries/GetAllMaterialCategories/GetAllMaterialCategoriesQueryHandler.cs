using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;

public sealed class GetAllMaterialCategoriesQueryHandler(
    IMaterialCategoryRepository materialCategoryRepository)
    : IQueryHandler<GetAllMaterialCategoriesQuery, List<MaterialCategoryResult>>
{
    public async Task<Result<List<MaterialCategoryResult>>> Handle(
        GetAllMaterialCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await materialCategoryRepository.GetAllWithParentAsync(
            cancellationToken);

        return categories.Select(MaterialCategoryResult.FromEntity).ToList();
    }
}
