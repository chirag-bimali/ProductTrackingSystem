namespace PTS.Domain.Entities;

public class Factory
{
    public Guid Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string PanNumber { get; private set; } = string.Empty;
    public DateTime EstablishedDate { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string ContactNum { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public Guid LocationId { get; private set; }
    public Location Location { get; private set; } = null!;

    public Guid FactoryAdminId { get; private set; }
    public User FactoryAdmin { get; private set; } = null!;

    private Factory() { }

    public Factory(
        string nameEn,
        string nameNp,
        string panNumber,
        DateTime establishedDate,
        string email,
        string contactNum,
        Guid locationId,
        Guid factoryAdminId)
    {
        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        PanNumber = panNumber;
        EstablishedDate = establishedDate;
        Email = email;
        ContactNum = contactNum;
        LocationId = locationId;
        FactoryAdminId = factoryAdminId;
        CreatedAt = DateTime.UtcNow;
    }
}