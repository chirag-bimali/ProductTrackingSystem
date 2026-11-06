using FluentValidation;
using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Localization.Roles;
using PTS.Application.Common.Localization.Users;
using PTS.Application.Features.Authentication.Commands.Register;

// using PTS.Domain.Constants;

namespace PTS.Application.Features.Authorization.Command.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private static readonly string[] AllowedRoles =
    [
        "Farmer",
        "FactoryAdmin",
        "SystemAdmin",
        "Admin",
    ];

    public RegisterCommandValidator(
        IUserRepository users,
        IStringLocalizer<RoleResource> roleLocalizer,
        IStringLocalizer<UserResource> userLocalizer
    )
    {
        RuleFor(x => x.NameEn)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage(userLocalizer["NameEnRequired"]);
        RuleFor(x => x.NameNp)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage(userLocalizer["NameNpRequired"]);

        RuleFor(x => x.Contact)
            .NotEmpty()
            .WithMessage(userLocalizer["ContactRequired"])
            .Matches(@"^9[78]\d{8}$")
            .WithMessage(userLocalizer["ContactInvalid"])
            .MustAsync(async (c, ct) => !await users.ContactExistsAsync(c, ct))
            .WithMessage(userLocalizer["ContactAlreadyRegistered"]);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(userLocalizer["EmailRequired"])
            .EmailAddress()
            .WithMessage(userLocalizer["InvalidEmail"])
            .MaximumLength(256)
            .WithMessage(userLocalizer["EmailMaxLength"])
            .MustAsync(async (e, ct) => !await users.EmailExistsAsync(e, ct))
            .WithMessage(userLocalizer["EmailAlreadyRegistered"]);


        // BCrypt only reads the first 72 bytes, so we cap the length there
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(userLocalizer["PasswordRequired"])
            .MinimumLength(8)
            .WithMessage(userLocalizer["PasswordMinLength"])
            .MaximumLength(72)
            .WithMessage(userLocalizer["PasswordMaxLength"])
            .Matches("[A-Z]")
            .WithMessage(userLocalizer["PasswordUppercaseRequired"])
            .Matches("[a-z]")
            .WithMessage(userLocalizer["PasswordLowercaseRequired"])
            .Matches("[0-9]")
            .WithMessage(userLocalizer["PasswordDigitRequired"]);





        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage(roleLocalizer["RoleRequired"])
            .Must(role => AllowedRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            .WithMessage(role => roleLocalizer["RoleNotAllowed", role.Role]);
    }
}
