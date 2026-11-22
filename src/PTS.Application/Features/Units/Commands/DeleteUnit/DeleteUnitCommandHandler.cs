using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.Units.Commands.DeleteUnit;

public sealed class DeleteUnitCommandHandler(
    IUnitRepository unitRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUnitCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        DeleteUnitCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null)
            return Error.NotFound("units.not_found", "The unit was not found.");

        var isAssignedToMaterial = await unitRepository.IsAssignedToMaterialAsync(
            request.UnitId,
            cancellationToken);

        if (isAssignedToMaterial)
        {
            return Error.Conflict(
                "units.in_use",
                "A unit assigned to materials cannot be deleted.");
        }

        unitRepository.Remove(unit);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }
}
