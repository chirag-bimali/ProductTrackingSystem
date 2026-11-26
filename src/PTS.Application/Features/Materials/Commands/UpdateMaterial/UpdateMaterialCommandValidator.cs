using FluentValidation;
using PTS.Application.Features.Materials.Commands.CreateMaterial;

namespace PTS.Application.Features.Materials.Commands.UpdateMaterial;

public sealed class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommand>
{
    public UpdateMaterialCommandValidator()
    {
        RuleFor(command => command.MaterialId)
            .NotEmpty()
            .WithMessage("Material ID is required.");

        RuleFor(command => command.NameEn)
            .NotEmpty()
            .WithMessage("English name is required.")
            .MaximumLength(200)
            .WithMessage("English name cannot exceed 200 characters.");

        RuleFor(command => command.NameNp)
            .NotEmpty()
            .WithMessage("Nepali name is required.")
            .MaximumLength(200)
            .WithMessage("Nepali name cannot exceed 200 characters.");

        RuleFor(command => command.MaterialCategoryId)
            .NotEmpty()
            .WithMessage("Material category ID is required.");

        RuleFor(command => command.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(command => command.Units)
            .NotEmpty()
            .WithMessage("At least one unit is required.")
            .Must(units => units is not null && units.Select(unit => unit.UnitId).Distinct().Count() == units.Count)
            .WithMessage("A unit cannot be selected more than once.")
            .Must(units => units is not null && units.Count(unit => unit.IsBaseUnit) == 1)
            .WithMessage("Exactly one base unit is required.");

        RuleForEach(command => command.Units).ChildRules(unit =>
        {
            unit.RuleFor(value => value.UnitId)
                .NotEmpty()
                .WithMessage("Unit ID is required.");
            unit.RuleFor(value => value.ConversionFactorToBaseUnit)
                .GreaterThan(0)
                .WithMessage("Conversion factor must be greater than zero.");
            unit.RuleFor(value => value)
                .Must(value => !value.IsBaseUnit || value.ConversionFactorToBaseUnit == 1m)
                .WithMessage("The base unit conversion factor must be 1.");
        });
    }
}
