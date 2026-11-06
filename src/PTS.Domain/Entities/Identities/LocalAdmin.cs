namespace PTS.Domain.Entities.Identities;

public class LocalAdmin
{
    public int MunicipalityId { get; set; }
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
    public Municipality Municipality { get; set; } = null!;
}
