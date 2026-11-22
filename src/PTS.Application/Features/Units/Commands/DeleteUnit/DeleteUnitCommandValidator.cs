using FluentValidation;

namespace PTS.Application.Features.Units.Commands.DeleteUnit;

public sealed class DeleteUnitCommandValidator : AbstractValidator<DeleteUnitCommand>
{
    public DeleteUnitCommandValidator()
    {
        RuleFor(command => command.UnitId)
            .NotEmpty()
            .WithMessage("Unit ID is required.");
    }
}
