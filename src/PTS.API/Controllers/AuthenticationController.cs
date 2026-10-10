// src/PTS.API/Controllers/AuthController.cs
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
  [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
  public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
  {
    var result = await sender.Send(command, ct);
    return Ok(result);
  }

  // [HttpPost("login")]
  // [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
  // public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
  // {
  //     var result = await sender.Send(command, ct);
  //     return Ok(result);
  // }
}