using FluentValidation;
using Microsoft.Extensions.Localization;
using PTS.Application.Common.Localization.Users;

namespace PTS.Application.Features.Users.Commands.CreateLocalAdmin;

public class CreateLocalAdminCommandValidator : AbstractValidator<CreateLocalAdminCommand>
{
    public CreateLocalAdminCommandValidator(IStringLocalizer<UserResource> userLocalizer)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(userLocalizer["NameEnRequired"])
            .MaximumLength(50)
            .WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(userLocalizer["NameNpRequired"])
            .MaximumLength(50)
            .WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(userLocalizer["EmailRequired"])
            .EmailAddress()
            .WithMessage(userLocalizer["EmailInvalid"]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(userLocalizer["PasswordRequired"])
            .MinimumLength(6)
            .WithMessage(userLocalizer["PasswordMinLength"])
            .MaximumLength(72)
            .WithMessage(userLocalizer["PasswordMaxLength"]);
    }
}
