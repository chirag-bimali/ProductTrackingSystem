using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.UnitCategories.Queries.GetAllUnitCategories;

public sealed class GetAllUnitCategoriesQueryHandler(
    IUnitCategoryRepository unitCategoryRepository)
    : IQueryHandler<GetAllUnitCategoriesQuery, List<UnitCategoryResult>>
{
    public async Task<Result<List<UnitCategoryResult>>> Handle(
        GetAllUnitCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await unitCategoryRepository.GetAllAsync(cancellationToken);

        return categories.Select(UnitCategoryResult.FromEntity).ToList();
    }
}
