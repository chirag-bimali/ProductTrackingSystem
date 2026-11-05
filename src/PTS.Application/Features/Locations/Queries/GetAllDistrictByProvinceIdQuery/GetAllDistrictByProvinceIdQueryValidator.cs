using FluentValidation;

namespace PTS.Application.Features.Locations.Queries.GetAllDistrictByProvinceIdQuery;

public class GetAllDistrictByProvinceIdQueryValidator
    : AbstractValidator<GetAllDistrictByProvinceIdQuery>
{
    public GetAllDistrictByProvinceIdQueryValidator()
    {
        RuleFor(x => x.ProvinceId)
            .GreaterThan(0)
            .WithMessage("Province ID must be a positive integer.");
    }
}
