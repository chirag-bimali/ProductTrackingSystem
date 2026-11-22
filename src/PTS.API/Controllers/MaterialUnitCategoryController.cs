using Microsoft.AspNetCore.Mvc;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.UnitCategories.Queries.GetAllUnitCategories;

namespace PTS.API.Controllers;

public sealed class MaterialUnitCategoryController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await dispatcher.DispatchQuery<
            GetAllUnitCategoriesQuery,
            List<UnitCategoryResult>>(
            new GetAllUnitCategoriesQuery(),
            ct);

        return FromResult(result);
    }
}
