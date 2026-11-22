using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Response;
using PTS.Domain.Entities.Materials;

namespace PTS.Application.Features.Materials.Commands.CreateMaterial;

public sealed class CreateMaterialCommandHandler(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMaterialCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateMaterialCommand request,
        CancellationToken cancellationToken)
    {
        var categoryExists = await materialRepository.MaterialCategoryExistsAsync(
            request.MaterialCategoryId,
            cancellationToken);

        if (!categoryExists)
        {
            return Error.NotFound(
                "material_categories.not_found",
                "The selected material category was not found.");
        }

        var unitIds = request.Units.Select(unit => unit.UnitId).ToList();
        var unitsExist = await materialRepository.UnitsExistAsync(unitIds, cancellationToken);

        if (!unitsExist)
            return Error.NotFound("units.not_found", "One or more selected units were not found.");

        var code = request.Code.Trim();
        var nameEn = request.NameEn.Trim();
        var nameNp = request.NameNp.Trim();
        var description = NormalizeOptional(request.Description);

        var hasConflict = await materialRepository.HasConflictAsync(
            code,
            nameEn,
            excludedMaterialId: null,
            cancellationToken);

        if (hasConflict)
        {
            return Error.Conflict(
                "materials.duplicate",
                "A material with the same code or English name already exists.");
        }

        var unitDefinitions = request.Units.Select(unit =>
            (unit.UnitId, unit.ConversionFactorToBaseUnit, unit.IsBaseUnit));

        var material = new Material(
            code,
            nameEn,
            nameNp,
            request.MaterialCategoryId,
            unitDefinitions,
            request.IsBatchTracked,
            request.IsSerialTracked,
            request.MinimumStockLevel,
            request.ReorderLevel,
            description);

        await materialRepository.AddAsync(material, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return material.Id;
    }

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
