using FluentValidation;

namespace PTS.Application.Features.MaterialCategories.Queries.GetMaterialCategoryById;

public sealed class GetMaterialCategoryByIdQueryValidator
    : AbstractValidator<GetMaterialCategoryByIdQuery>
{
    public GetMaterialCategoryByIdQueryValidator()
    {
        RuleFor(query => query.MaterialCategoryId)
            .NotEmpty()
            .WithMessage("Material category ID is required.");
    }
}
