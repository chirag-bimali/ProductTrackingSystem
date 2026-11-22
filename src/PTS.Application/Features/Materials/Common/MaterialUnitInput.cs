namespace PTS.Application.Features.Materials.Common;

public sealed record MaterialUnitInput(
    Guid UnitId,
    decimal ConversionFactorToBaseUnit,
    bool IsBaseUnit);
