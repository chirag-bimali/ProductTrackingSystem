using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Abstractions.Security;
using PTS.Application.Common.Localization.Users;
using PTS.Application.Common.Response;
using PTS.Domain.Entities;

namespace PTS.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler(
    IUserRepository repository,
    IJwtTokenService jwtTokenService,
    IPasswordHasher passwordHasher,
    IStringLocalizer<UserResource> userLocalizer
) : ICommandHandler<LoginCommand, GeneratedToken>
{
    public async Task<Result<GeneratedToken>> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = !string.IsNullOrWhiteSpace(command.Email)
            ? await repository.GetByEmailAsync(command.Email, ct)
            : await repository.GetByContactAsync(command.Contact!, ct);

        if (user == null)
        {
            return Error.Unauthorized("invalid_credentials", userLocalizer["InvalidCredentials"]);
        }

        if (!passwordHasher.Verify(command.Password, user.PasswordHash))
        {
            return Error.Unauthorized("invalid_credentials", userLocalizer["InvalidCredentials"]);
        }
        var token = jwtTokenService.GenerateAccessToken(
            user,
            user.UserRoles.Select(ur => ur.Role.NameEn).ToList()
        );

        return new GeneratedToken(token.Token, token.ExpiresAt);
    }
}
