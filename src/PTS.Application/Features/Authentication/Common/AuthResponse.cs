namespace PTS.Application.Features.Authorization;

public record AuthResponse(Guid UserId, string AccessToken, bool IsVerified);