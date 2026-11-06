namespace PTS.Domain.Entities.Materials;

public class MaterialBatch
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public Material Material { get; private set; } = null!;

    public string BatchNumber { get; private set; } = string.Empty;
    public DateTime? ManufactureDate { get; private set; }
    public DateTime? ExpiryDate { get; private set; }
    public DateTime ReceivedDate { get; private set; }
    public decimal InitialQuantity { get; private set; }
    public decimal CurrentQuantity { get; private set; }
    public decimal? UnitCost { get; private set; }
    public string? SupplierInfo { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<MaterialSerialNumber> SerialNumbers { get; private set; } = new List<MaterialSerialNumber>();

    private MaterialBatch() { }

    public MaterialBatch(
        Guid materialId,
        string batchNumber,
        decimal initialQuantity,
        DateTime receivedDate,
        DateTime? manufactureDate = null,
        DateTime? expiryDate = null,
        decimal? unitCost = null,
        string? supplierInfo = null,
        string? notes = null)
    {
        if (materialId == Guid.Empty)
            throw new ArgumentException("Material ID is required.", nameof(materialId));
        if (string.IsNullOrWhiteSpace(batchNumber))
            throw new ArgumentException("Batch number cannot be empty.", nameof(batchNumber));
        if (initialQuantity < 0)
            throw new ArgumentException("Initial quantity cannot be negative.", nameof(initialQuantity));
        if (unitCost.HasValue && unitCost.Value < 0)
            throw new ArgumentException("Unit cost cannot be negative.", nameof(unitCost));
        if (manufactureDate.HasValue && expiryDate.HasValue && expiryDate.Value < manufactureDate.Value)
            throw new ArgumentException("Expiry date cannot be earlier than manufacture date.");

        Id = Guid.NewGuid();
        MaterialId = materialId;
        BatchNumber = batchNumber;
        InitialQuantity = initialQuantity;
        CurrentQuantity = initialQuantity;
        ReceivedDate = receivedDate;
        ManufactureDate = manufactureDate;
        ExpiryDate = expiryDate;
        UnitCost = unitCost;
        SupplierInfo = supplierInfo;
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
    }

    public void AdjustQuantity(decimal quantityDelta)
    {
        var newQuantity = CurrentQuantity + quantityDelta;
        if (newQuantity < 0)
            throw new InvalidOperationException("Current quantity cannot drop below zero.");

        CurrentQuantity = newQuantity;
    }

    public void UpdateBatchDetails(
        DateTime? manufactureDate,
        DateTime? expiryDate,
        decimal? unitCost,
        string? supplierInfo,
        string? notes)
    {
        if (unitCost.HasValue && unitCost.Value < 0)
            throw new ArgumentException("Unit cost cannot be negative.", nameof(unitCost));
        if (manufactureDate.HasValue && expiryDate.HasValue && expiryDate.Value < manufactureDate.Value)
            throw new ArgumentException("Expiry date cannot be earlier than manufacture date.");

        ManufactureDate = manufactureDate;
        ExpiryDate = expiryDate;
        UnitCost = unitCost;
        SupplierInfo = supplierInfo;
        Notes = notes;
    }
}
