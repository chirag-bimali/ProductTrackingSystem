// src/PTS.API/Controllers/AuthController.cs
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.API.Responses;

// using PTS.Application.Features.Authurization.Login;
// using PTS.Application.Features.Auth.Login;
// using PTS.Application.Features.Auth.Register;
using PTS.Application.Features.Authorization;
using PTS.Application.Features.Authorization.Command.Register;

namespace PTS.API.Controllers;

[AllowAnonymous]
public class AuthController(ISender sender) : ApiControllerBase
{
  [HttpPost("register")]
  [ProducesResponseType<ApiResponse<RegisterResult>>(StatusCodes.Status200OK)]
  [ProducesResponseType<ApiResponse<object>>(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    => HandleResult(await sender.Send(command, ct));

}