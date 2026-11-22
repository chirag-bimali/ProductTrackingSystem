using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using MeasurementUnit = PTS.Domain.Entities.Materials.Unit;

namespace PTS.Application.Features.Units.Commands.CreateUnit;

public sealed class CreateUnitCommandHandler(
    IUnitRepository unitRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUnitCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUnitCommand request,
        CancellationToken cancellationToken)
    {
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
            excludedUnitId: null,
            cancellationToken);

        if (hasConflict)
        {
            return Error.Conflict(
                "units.duplicate",
                "A unit with the same English name or abbreviation already exists in this category.");
        }

        var unit = new MeasurementUnit(
            nameEn,
            nameNp,
            abbreviation,
            request.UnitCategoryId);

        await unitRepository.AddAsync(unit, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return unit.Id;
    }
}
