using FluentValidation;

namespace PTS.Application.Features.Locations.Queries.GetAllWardByMunicipalityIdQuery;

public class GetAllWardByMunicipalityIdQueryValidator
    : AbstractValidator<GetAllWardByMunicipalityIdQuery>
{
    public GetAllWardByMunicipalityIdQueryValidator()
    {
        RuleFor(x => x.MunicipalityId)
            .NotEmpty()
            .WithMessage("Municipality Id is required.")
            .GreaterThan(0)
            .WithMessage("Municipality Id must be greater than 0.");
    }
}
