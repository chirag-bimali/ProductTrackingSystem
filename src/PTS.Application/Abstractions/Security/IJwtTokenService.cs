using PTS.Domain.Entities.Identities;

namespace PTS.Application.Abstractions.Security;

public interface IJwtTokenService
{
    public GeneratedToken GenerateAccessToken(User user, IEnumerable<string> roles);
}
