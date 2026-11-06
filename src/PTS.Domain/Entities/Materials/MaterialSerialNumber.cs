namespace PTS.Domain.Entities.Materials;

public class MaterialSerialNumber
{
    public Guid Id { get; private set; }
    public Guid MaterialId { get; private set; }
    public Material Material { get; private set; } = null!;

    public Guid? MaterialBatchId { get; private set; }
    public MaterialBatch? MaterialBatch { get; private set; }

    public string SerialNumber { get; private set; } = string.Empty;
    public string Status { get; private set; } = "Available";
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MaterialSerialNumber() { }

    public MaterialSerialNumber(
        Guid materialId,
        string serialNumber,
        Guid? materialBatchId = null,
        string status = "Available",
        string? notes = null)
    {
        if (materialId == Guid.Empty)
            throw new ArgumentException("Material ID is required.", nameof(materialId));
        if (string.IsNullOrWhiteSpace(serialNumber))
            throw new ArgumentException("Serial number cannot be empty.", nameof(serialNumber));
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty.", nameof(status));

        Id = Guid.NewGuid();
        MaterialId = materialId;
        SerialNumber = serialNumber;
        MaterialBatchId = materialBatchId;
        Status = status;
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(string status, string? notes = null)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be empty.", nameof(status));

        Status = status;
        if (notes != null)
            Notes = notes;
    }

    public void AssignBatch(Guid materialBatchId)
    {
        if (materialBatchId == Guid.Empty)
            throw new ArgumentException("Batch ID is required.", nameof(materialBatchId));

        MaterialBatchId = materialBatchId;
    }
}
