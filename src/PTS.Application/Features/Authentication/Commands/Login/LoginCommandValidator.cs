using FluentValidation;
using Microsoft.Extensions.Localization;
using PTS.Application.Common.Localization.Users;

namespace PTS.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IStringLocalizer<UserResource> userLocalizer)
    {
        // I want either Email or Contact to be provided, but not both. If both are provided, it should be invalid.
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.Contact))
            .WithMessage(userLocalizer["EmailOrContactRequired"]);
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage(userLocalizer["InvalidEmail"]);
        RuleFor(x => x.Password).NotEmpty();
    }
}
