using FluentValidation;

namespace PTS.Application.Features.Units.Commands.CreateUnit;

public sealed class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(command => command.NameEn)
            .NotEmpty()
            .WithMessage("English name is required.")
            .MaximumLength(100)
            .WithMessage("English name cannot exceed 100 characters.");

        RuleFor(command => command.NameNp)
            .NotEmpty()
            .WithMessage("Nepali name is required.")
            .MaximumLength(100)
            .WithMessage("Nepali name cannot exceed 100 characters.");

        RuleFor(command => command.Abbreviation)
            .NotEmpty()
            .WithMessage("Abbreviation is required.")
            .MaximumLength(20)
            .WithMessage("Abbreviation cannot exceed 20 characters.");

        RuleFor(command => command.UnitCategoryId)
            .NotEmpty()
            .WithMessage("Unit category ID is required.");
    }
}
