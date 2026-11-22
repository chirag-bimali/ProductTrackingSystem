using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.Units.Commands.UpdateUnit;

public sealed class UpdateUnitCommandHandler(
    IUnitRepository unitRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUnitCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        UpdateUnitCommand request,
        CancellationToken cancellationToken)
    {
        var unit = await unitRepository.GetByIdAsync(request.UnitId, cancellationToken);

        if (unit is null)
            return Error.NotFound("units.not_found", "The unit was not found.");

        var categoryExists = await unitRepository.UnitCategoryExistsAsync(
            request.UnitCategoryId,
            cancellationToken);

        if (!categoryExists)
        {
            return Error.NotFound(
                "unit_categories.not_found",
                "The selected unit category was not found.");
        }

        var nameEn = request.NameEn.Trim();
        var nameNp = request.NameNp.Trim();
        var abbreviation = request.Abbreviation.Trim();

        var hasConflict = await unitRepository.HasConflictAsync(
            nameEn,
            abbreviation,
            request.UnitCategoryId,
            request.UnitId,
            cancellationToken);

        if (hasConflict)
        {
            return Error.Conflict(
                "units.duplicate",
                "A unit with the same English name or abbreviation already exists in this category.");
        }

        unit.Update(nameEn, nameNp, abbreviation, request.UnitCategoryId);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }
}
