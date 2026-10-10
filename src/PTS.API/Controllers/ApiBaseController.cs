// src/PTS.API/Controllers/ApiControllerBase.cs
using Microsoft.AspNetCore.Mvc;

namespace PTS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
}