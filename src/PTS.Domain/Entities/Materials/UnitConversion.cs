namespace PTS.Domain.Entities.Materials;

public class UnitConversion
{
    public Guid Id { get; private set; }
    public Guid FromUnitId { get; private set; }
    public Unit FromUnit { get; private set; } = null!;

    public Guid ToUnitId { get; private set; }
    public Unit ToUnit { get; private set; } = null!;

    public decimal Factor { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private UnitConversion() { }

    public UnitConversion(Guid fromUnitId, Guid toUnitId, decimal factor)
    {
        if (fromUnitId == Guid.Empty)
            throw new ArgumentException("FromUnit ID is required.", nameof(fromUnitId));
        if (toUnitId == Guid.Empty)
            throw new ArgumentException("ToUnit ID is required.", nameof(toUnitId));
        if (fromUnitId == toUnitId)
            throw new ArgumentException("FromUnit and ToUnit cannot be the same.");
        if (factor <= 0)
            throw new ArgumentException("Conversion factor must be greater than zero.", nameof(factor));

        Id = Guid.NewGuid();
        FromUnitId = fromUnitId;
        ToUnitId = toUnitId;
        Factor = factor;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateFactor(decimal factor)
    {
        if (factor <= 0)
            throw new ArgumentException("Conversion factor must be greater than zero.", nameof(factor));

        Factor = factor;
    }
}
