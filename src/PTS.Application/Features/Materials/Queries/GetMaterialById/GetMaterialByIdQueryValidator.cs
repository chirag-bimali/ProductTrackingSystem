using FluentValidation;

namespace PTS.Application.Features.Materials.Queries.GetMaterialById;

public sealed class GetMaterialByIdQueryValidator : AbstractValidator<GetMaterialByIdQuery>
{
    public GetMaterialByIdQueryValidator()
    {
        RuleFor(query => query.MaterialId)
            .NotEmpty()
            .WithMessage("Material ID is required.");
    }
}
