using FluentValidation;

namespace PTS.Application.Features.MaterialCategories.Commands.UpdateMaterialCategory;

public sealed class UpdateMaterialCategoryCommandValidator
    : AbstractValidator<UpdateMaterialCategoryCommand>
{
    public UpdateMaterialCategoryCommandValidator()
    {
        RuleFor(command => command.MaterialCategoryId)
            .NotEmpty()
            .WithMessage("Material category ID is required.");

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

        RuleFor(command => command.Code)
            .MaximumLength(50)
            .WithMessage("Code cannot exceed 50 characters.");

        RuleFor(command => command.Description)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(command => command.ParentCategoryId)
            .Must(parentId => !parentId.HasValue || parentId.Value != Guid.Empty)
            .WithMessage("Parent category ID cannot be empty.");
    }
}
