// src/PTS.API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Security;
using PTS.Application.Features.Authentication.Commands.Login;
using PTS.Application.Features.Authentication.Commands.Register;

namespace PTS.API.Controllers;

[AllowAnonymous]
public class AuthController(IDispatcher dispatcher) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<RegisterCommand, RegisterResult>(command, ct);
        return FromResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await dispatcher.DispatchCommand<LoginCommand, GeneratedToken>(command, ct);
        return FromResult(result);
    }
}
