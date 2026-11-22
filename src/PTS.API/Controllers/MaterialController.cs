using Microsoft.AspNetCore.Mvc;
using PTS.API.Contracts.Materials;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.Materials.Commands.CreateMaterial;
using PTS.Application.Features.Materials.Commands.DeleteMaterial;
using PTS.Application.Features.Materials.Commands.UpdateMaterial;
using PTS.Application.Features.Materials.Queries.GetAllMaterials;
using PTS.Application.Features.Materials.Queries.GetMaterialById;

namespace PTS.API.Controllers;

public sealed class MaterialController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<GetAllMaterialsQuery, List<MaterialResult>>(
            new GetAllMaterialsQuery(),
            ct);

        return FromResult(result);
    }

    [HttpGet("{materialId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid materialId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<GetMaterialByIdQuery, MaterialResult>(
            new GetMaterialByIdQuery(materialId),
            ct);

        return FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMaterialCommand command,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<CreateMaterialCommand, Guid>(command, ct);

        return FromResult(result);
    }

    [HttpPut("{materialId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid materialId,
        [FromBody] UpdateMaterialRequest request,
        CancellationToken ct)
    {
        var command = new UpdateMaterialCommand(
            materialId,
            request.Code,
            request.NameEn,
            request.NameNp,
            request.MaterialCategoryId,
            request.IsBatchTracked,
            request.IsSerialTracked,
            request.MinimumStockLevel,
            request.ReorderLevel,
            request.Description,
            request.Units);

        var result = await dispatcher.DispatchCommand<UpdateMaterialCommand, Unit>(command, ct);

        return FromNoContent(result);
    }

    [HttpDelete("{materialId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid materialId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<DeleteMaterialCommand, Unit>(
            new DeleteMaterialCommand(materialId),
            ct);

        return FromNoContent(result);
    }
}
