using FluentValidation;

namespace PTS.Application.Features.Units.Queries.GetUnitById;

public sealed class GetUnitByIdQueryValidator : AbstractValidator<GetUnitByIdQuery>
{
    public GetUnitByIdQueryValidator()
    {
        RuleFor(query => query.UnitId)
            .NotEmpty()
            .WithMessage("Unit ID is required.");
    }
}
