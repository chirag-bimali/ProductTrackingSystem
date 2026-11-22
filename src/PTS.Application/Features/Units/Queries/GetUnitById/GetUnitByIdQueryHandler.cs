using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Application.Features.Units.Queries.GetAllUnits;

namespace PTS.Application.Features.Units.Queries.GetUnitById;

public sealed class GetUnitByIdQueryHandler(IUnitRepository unitRepository)
    : IQueryHandler<GetUnitByIdQuery, UnitResult>
{
    public async Task<Result<UnitResult>> Handle(
        GetUnitByIdQuery request,
        CancellationToken cancellationToken)
    {
        var unit = await unitRepository.GetByIdWithCategoryAsync(
            request.UnitId,
            cancellationToken);

        if (unit is null)
            return Error.NotFound("units.not_found", "The unit was not found.");

        return UnitResult.FromEntity(unit);
    }
}
