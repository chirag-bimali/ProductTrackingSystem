using FluentValidation;

namespace PTS.Application.Features.Locations.Queries.GetAllMunicipalityByDistrictIdQuery;

public class GetAllMunicipalityByDistrictIdQueryValidator
    : AbstractValidator<GetAllMunicipalityByDistrictIdQuery>
{
    public GetAllMunicipalityByDistrictIdQueryValidator()
    {
        RuleFor(x => x.DistrictId)
            .NotEmpty()
            .WithMessage("District Id is required.")
            .GreaterThan(0)
            .WithMessage("District Id must be greater than 0.");
    }
}
