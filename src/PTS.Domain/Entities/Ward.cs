namespace PTS.Domain.Entities;

public class Ward
{
  public int Id { get; private set; }

  // The ward number as people say it, e.g. 1, 2, 3
  public int Number { get; private set; }

  public int MunicipalityId { get; private set; }
  public Municipality Municipality { get; private set; } = null!;

  private Ward() { }

  public Ward(int id, int municipalityId, int number)
  {
    if (number <= 0)
      throw new ArgumentException("Ward number must be greater than zero.", nameof(number));

    Id = id;
    MunicipalityId = municipalityId;
    Number = number;
  }
}