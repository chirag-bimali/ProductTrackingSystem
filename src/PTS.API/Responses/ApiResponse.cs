// src/PTS.API/Responses/ApiResponse.cs
using System.Text.Json.Serialization;
using PTS.Application.Common.Response;

namespace PTS.API.Responses;

public sealed class ApiResponse<T>
{
    public bool Success { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ApiError? Error { get; init; }

    public static ApiResponse<T> Ok(T data) => new() { Success = true, Data = data };

    public static ApiResponse<T> Fail(Error error) => new()
    {
        Success = false,
        Error = new ApiError(error.Code, error.Message, error.Details, error.RowErrors)
    };
}

public sealed record ApiError(
    string Code,
    string Message,
    Dictionary<string, string[]>? Details = null,
    Dictionary<int, Dictionary<string, string[]>>? RowErrors = null);