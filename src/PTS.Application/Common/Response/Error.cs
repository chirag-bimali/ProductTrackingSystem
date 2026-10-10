// src/PTS.Application/Common/Response/Error.cs
namespace PTS.Application.Common.Response;

public sealed record Error(
    ErrorType Type,
    string Code,
    string Message,
    Dictionary<string, string[]>? Details = null,
    Dictionary<int, Dictionary<string, string[]>>? RowErrors = null)
{
  public static Error Validation(string code, string message, Dictionary<string, string[]>? details = null)
      => new(ErrorType.Validation, code, message, details);

  public static Error Unauthorized(string code, string message) => new(ErrorType.Unauthorized, code, message);
  public static Error Forbidden(string code, string message) => new(ErrorType.Forbidden, code, message);
  public static Error NotFound(string code, string message) => new(ErrorType.NotFound, code, message);
  public static Error Conflict(string code, string message) => new(ErrorType.Conflict, code, message);
  public static Error Failure(string code, string message) => new(ErrorType.Failure, code, message);
}