using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using CommandUnit = PTS.Application.Abstractions.Messaging.Unit;

namespace PTS.Application.Features.Materials.Commands.UpdateMaterial;

public sealed class UpdateMaterialCommandHandler(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateMaterialCommand>
{
    public async Task<Result<CommandUnit>> Handle(
        UpdateMaterialCommand request,
        CancellationToken cancellationToken
    )
    {
        var material = await materialRepository.GetByIdForUpdateAsync(
            request.MaterialId,
            cancellationToken
        );

        if (material is null)
            return Error.NotFound("materials.not_found", "The material was not found.");

        var categoryExists = await materialRepository.MaterialCategoryExistsAsync(
            request.MaterialCategoryId,
            cancellationToken
        );

        if (!categoryExists)
        {
            return Error.NotFound(
                "material_categories.not_found",
                "The selected material category was not found."
            );
        }

        var unitIds = request.Units.Select(unit => unit.UnitId).ToList();
        var unitsExist = await materialRepository.UnitsExistAsync(unitIds, cancellationToken);

        if (!unitsExist)
            return Error.NotFound("units.not_found", "One or more selected units were not found.");

        var existingBaseUnitId = material.MaterialUnits.Single(unit => unit.IsBaseUnit).UnitId;
        var requestedBaseUnitId = request.Units.Single(unit => unit.IsBaseUnit).UnitId;

        if (existingBaseUnitId != requestedBaseUnitId)
        {
            return Error.Validation(
                "materials.base_unit_immutable",
                "The material base unit cannot be changed after creation."
            );
        }

        var nameEn = request.NameEn.Trim();
        var nameNp = request.NameNp.Trim();
        var description = NormalizeOptional(request.Description);

        var hasConflict = await materialRepository.HasConflictAsync(
            nameEn,
            request.MaterialId,
            cancellationToken
        );

        if (hasConflict)
        {
            return Error.Conflict(
                "materials.duplicate",
                "A material with the same code or English name already exists."
            );
        }

        material.Update(nameEn, nameNp, request.MaterialCategoryId, description);

        request.Units.Select(unit =>
            (unit.UnitId, unit.ConversionFactorToBaseUnit, unit.IsBaseUnit)
        );

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandUnit.Value;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
