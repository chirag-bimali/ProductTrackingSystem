using Microsoft.AspNetCore.Mvc;
using PTS.API.Contracts.Units;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.Units.Commands.CreateUnit;
using PTS.Application.Features.Units.Commands.DeleteUnit;
using PTS.Application.Features.Units.Commands.UpdateUnit;
using PTS.Application.Features.Units.Queries.GetAllUnits;
using PTS.Application.Features.Units.Queries.GetUnitById;

namespace PTS.API.Controllers;

public sealed class UnitController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<GetAllUnitsQuery, List<UnitResult>>(
            new GetAllUnitsQuery(),
            ct);

        return FromResult(result);
    }

    [HttpGet("{unitId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid unitId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<GetUnitByIdQuery, UnitResult>(
            new GetUnitByIdQuery(unitId),
            ct);

        return FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUnitCommand command,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<CreateUnitCommand, Guid>(command, ct);

        return FromResult(result);
    }

    [HttpPut("{unitId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid unitId,
        [FromBody] UpdateUnitRequest request,
        CancellationToken ct)
    {
        var command = new UpdateUnitCommand(
            unitId,
            request.NameEn,
            request.NameNp,
            request.Abbreviation,
            request.UnitCategoryId);

        var result = await dispatcher.DispatchCommand<UpdateUnitCommand, Unit>(command, ct);

        return FromNoContent(result);
    }

    [HttpDelete("{unitId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid unitId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<DeleteUnitCommand, Unit>(
            new DeleteUnitCommand(unitId),
            ct);

        return FromNoContent(result);
    }
}
