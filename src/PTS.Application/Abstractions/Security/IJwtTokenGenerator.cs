using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Security;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user, string roleName);
}