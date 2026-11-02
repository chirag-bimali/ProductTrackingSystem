using FluentValidation;
using PTS.Application.Abstractions.Persistence;

// using PTS.Domain.Constants;

namespace PTS.Application.Features.Authorization.Command.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private static readonly string[] AllowedRoles = ["Farmer", "FactoryAdmin"];

    public RegisterCommandValidator(IUserRepository users)
    {
        RuleFor(x => x.NameEn).NotEmpty().MaximumLength(200);
        RuleFor(x => x.NameNp).NotEmpty().MaximumLength(200);

        RuleFor(x => x.Contact)
            .NotEmpty()
            .Matches(@"^9[78]\d{8}$")
            .WithMessage("Contact must be a 10-digit mobile number starting with 97 or 98.")
            .MustAsync(async (c, ct) => !await users.ContactExistsAsync(c, ct))
            .WithMessage("This contact number is already registered.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .MustAsync(async (e, ct) => !await users.EmailExistsAsync(e, ct))
            .WithMessage("This email is already registered.");

        // BCrypt only reads the first 72 bytes, so we cap the length there
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(72)
            .Matches("[A-Z]")
            .WithMessage("Password needs at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password needs at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password needs at least one digit.");

        RuleFor(x => x.Role)
            .NotEmpty()
            .Must(role => AllowedRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            .WithMessage("This role cannot be self-registered.");
    }
}
