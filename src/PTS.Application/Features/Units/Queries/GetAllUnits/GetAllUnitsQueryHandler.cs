using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Units.Queries.GetAllUnits;

public sealed class GetAllUnitsQueryHandler(IUnitRepository unitRepository)
    : IQueryHandler<GetAllUnitsQuery, List<UnitResult>>
{
    public async Task<Result<List<UnitResult>>> Handle(
        GetAllUnitsQuery request,
        CancellationToken cancellationToken)
    {
        var units = await unitRepository.GetAllWithCategoryAsync(cancellationToken);

        return units.Select(UnitResult.FromEntity).ToList();
    }
}
