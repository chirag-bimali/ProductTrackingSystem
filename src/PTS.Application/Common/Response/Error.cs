namespace PTS.Application.Common.Response;
public sealed record Error(
    string Code,
    string Message,
    Dictionary<string, string[]>? Details = null,
    Dictionary<int, Dictionary<string, string[]>>? RowErrors = null
);