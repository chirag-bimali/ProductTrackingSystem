using FluentValidation;

namespace PTS.Application.Features.Materials.Commands.DeleteMaterial;

public sealed class DeleteMaterialCommandValidator : AbstractValidator<DeleteMaterialCommand>
{
    public DeleteMaterialCommandValidator()
    {
        RuleFor(command => command.MaterialId)
            .NotEmpty()
            .WithMessage("Material ID is required.");
    }
}
