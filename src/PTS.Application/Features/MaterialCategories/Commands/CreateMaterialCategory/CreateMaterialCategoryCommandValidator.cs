using FluentValidation;

namespace PTS.Application.Features.MaterialCategories.Commands.CreateMaterialCategory;

public sealed class CreateMaterialCategoryCommandValidator
    : AbstractValidator<CreateMaterialCategoryCommand>
{
    public CreateMaterialCategoryCommandValidator()
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
