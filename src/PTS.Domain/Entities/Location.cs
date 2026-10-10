namespace PTS.Domain.Entities;

public class Location
{
    public Guid Id { get; private set; }

    public int MunicipalityId { get; private set; }
    public Municipality Municipality { get; private set; } = null!;

    public int WardId { get; private set; }
    public Ward Ward { get; private set; } = null!;

    public string? AddressLine { get; private set; }
    public string? GpsCoordinates { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private Location() { }

    public Location(int municipalityId, int ward,
        string? addressLine = null, string? gpsCoordinates = null)
    {
        Id = Guid.NewGuid();
        MunicipalityId = municipalityId;
        WardId = ward;
        AddressLine = addressLine;
        GpsCoordinates = gpsCoordinates;
        CreatedAt = DateTime.UtcNow;
    }
}