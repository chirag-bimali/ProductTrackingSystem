// src/PTS.Application/Common/Response/ErrorType.cs
namespace PTS.Application.Common.Response;

public enum ErrorType
{
  Validation,
  Unauthorized,
  Forbidden,
  NotFound,
  Conflict,
  Failure
}