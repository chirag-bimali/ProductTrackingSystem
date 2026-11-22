using Microsoft.AspNetCore.Mvc;
using PTS.API.Contracts.MaterialCategories;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.MaterialCategories.Commands.CreateMaterialCategory;
using PTS.Application.Features.MaterialCategories.Commands.DeleteMaterialCategory;
using PTS.Application.Features.MaterialCategories.Commands.UpdateMaterialCategory;
using PTS.Application.Features.MaterialCategories.Queries.GetAllMaterialCategories;
using PTS.Application.Features.MaterialCategories.Queries.GetMaterialCategoryById;

namespace PTS.API.Controllers;

public sealed class MaterialCategoryController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<
            GetAllMaterialCategoriesQuery,
            List<MaterialCategoryResult>>(
            new GetAllMaterialCategoriesQuery(),
            ct);

        return FromResult(result);
    }

    [HttpGet("{materialCategoryId:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid materialCategoryId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<
            GetMaterialCategoryByIdQuery,
            MaterialCategoryResult>(
            new GetMaterialCategoryByIdQuery(materialCategoryId),
            ct);

        return FromResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMaterialCategoryCommand command,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<CreateMaterialCategoryCommand, Guid>(
            command,
            ct);

        return FromResult(result);
    }

    [HttpPut("{materialCategoryId:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid materialCategoryId,
        [FromBody] UpdateMaterialCategoryRequest request,
        CancellationToken ct)
    {
        var command = new UpdateMaterialCategoryCommand(
            materialCategoryId,
            request.NameEn,
            request.NameNp,
            request.Code,
            request.Description,
            request.ParentCategoryId);

        var result = await dispatcher.DispatchCommand<UpdateMaterialCategoryCommand, Unit>(
            command,
            ct);

        return FromNoContent(result);
    }

    [HttpDelete("{materialCategoryId:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid materialCategoryId,
        CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<DeleteMaterialCategoryCommand, Unit>(
            new DeleteMaterialCategoryCommand(materialCategoryId),
            ct);

        return FromNoContent(result);
    }
}
