using PTS.Domain.Entities;
using SE.Application.Abstractions.Security;

namespace PTS.Application.Abstractions.Security;

public interface IJwtTokenService
{
    public GeneratedToken GenerateAccessToken(User user, IEnumerable<string> roles);
}
