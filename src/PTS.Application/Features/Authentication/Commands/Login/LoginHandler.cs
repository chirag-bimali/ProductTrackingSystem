using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Abstractions.Security;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Authentication.Commands.Login;

public class LoginHandler(
// IUserRepository repository,
// IJwtTokenService jwtTokenService,
// IPasswordHasher passwordHasher
) : ICommandHandler<LoginCommand, GeneratedToken>
{
    public async Task<Result<GeneratedToken>> Handle(LoginCommand command, CancellationToken ct)
    {
        return new GeneratedToken("sample_token", DateTime.UtcNow.AddHours(1));
    }
}
