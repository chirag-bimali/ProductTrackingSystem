using FluentValidation;

namespace PTS.Application.Features.Authentication.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        // I want either Email or Contact to be provided, but not both. If both are provided, it should be invalid.
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.Contact))
            .WithMessage("Either email or phone number is required.");
        RuleFor(x => x.Password).NotEmpty();
    }
}
