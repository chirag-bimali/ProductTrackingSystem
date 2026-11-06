using Microsoft.AspNetCore.Mvc;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Features.Users.Commands.CreateLocalAdmin;
using PTS.Application.Features.Users.Queries.GetAllLocalAdmin;
using PTS.Application.Features.Users.Queries.GetMe;

namespace PTS.API.Controllers;

public class UserController(
    ICommandHandler<CreateLocalAdminCommand, Guid> createLocalAdminCommandHandler,
    IQueryHandler<GetAllLocalAdminQuery, GetAllLocalAdminQueryResult> getAllLocalAdminQueryHandler
) : ApiControllerBase
{
    [HttpPost("LocalAdmin")]
    public async Task<IActionResult> CreateLocalAdmin([FromBody] CreateLocalAdminCommand command)
    {
        var result = await createLocalAdminCommandHandler.Handle(
            command,
            HttpContext.RequestAborted
        );
        return FromResult(result);
    }

    [HttpGet("LocalAdmin")]
    public async Task<IActionResult> GetAllLocalAdmins(
        [FromQuery] GetAllLocalAdminQuery query,
        CancellationToken cancellationToken
    )
    {
        var result = await getAllLocalAdminQueryHandler.Handle(query, cancellationToken);
        return FromResult(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe(
        [FromServices] IQueryHandler<GetMeQuery, GetMeQueryResult> getMeQueryHandler,
        CancellationToken cancellationToken
    )
    {
        var result = await getMeQueryHandler.Handle(new GetMeQuery(), cancellationToken);
        return FromResult(result);
    }
}
