// src/PTS.API/Controllers/ApiControllerBase.cs
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Common.Response;
using PTS.API.Responses;

namespace PTS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
  protected IActionResult HandleResult<T>(Result<T> result)
  {
    if (result.IsSuccess)
      return Ok(ApiResponse<T>.Ok(result.Value!));

    Error error = result.Error!;

    int status = error.Type switch
    {
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      _ => StatusCodes.Status500InternalServerError
    };

    return StatusCode(status, ApiResponse<object>.Fail(error));
  }
}