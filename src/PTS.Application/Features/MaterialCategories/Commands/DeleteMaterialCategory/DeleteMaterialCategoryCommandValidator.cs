using FluentValidation;

namespace PTS.Application.Features.MaterialCategories.Commands.DeleteMaterialCategory;

public sealed class DeleteMaterialCategoryCommandValidator
    : AbstractValidator<DeleteMaterialCategoryCommand>
{
    public DeleteMaterialCategoryCommandValidator()
    {
        RuleFor(command => command.MaterialCategoryId)
            .NotEmpty()
            .WithMessage("Material category ID is required.");
    }
}
