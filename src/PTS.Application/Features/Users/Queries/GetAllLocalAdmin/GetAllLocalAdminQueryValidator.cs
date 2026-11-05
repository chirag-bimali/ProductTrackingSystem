using FluentValidation;

namespace PTS.Application.Features.Users.Queries.GetAllLocalAdmin;

public class GetAllLocalAdminQueryValidator : AbstractValidator<GetAllLocalAdminQuery>
{
    public GetAllLocalAdminQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(query => query.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");
    }
}
