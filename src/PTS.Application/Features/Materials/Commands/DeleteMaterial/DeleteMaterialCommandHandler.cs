using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.Materials.Commands.DeleteMaterial;

public sealed class DeleteMaterialCommandHandler(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMaterialCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        DeleteMaterialCommand request,
        CancellationToken cancellationToken)
    {
        var material = await materialRepository.GetByIdAsync(
            request.MaterialId,
            cancellationToken);

        if (material is null)
            return Error.NotFound("materials.not_found", "The material was not found.");

        materialRepository.Remove(material);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }
}
