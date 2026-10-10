namespace PTS.Domain.Entities;

public class Role
{
  public Guid Id { get; private set; }
  public string NameEn { get; private set; } = string.Empty;
  public string NameNp { get; private set; } = string.Empty;

  // Navigation property for EF Core
  public ICollection<User> Users { get; private set; } = new List<User>();

  // EF Core constructor
  private Role() { }

  public Role(string nameEn, string nameNp)
  {
    if (string.IsNullOrWhiteSpace(nameEn))
      throw new ArgumentException("Role name cannot be empty.", nameof(nameEn));
    if (string.IsNullOrWhiteSpace(nameNp))
      throw new ArgumentException("Role name cannot be empty.", nameof(nameNp));

    Id = Guid.NewGuid();
    NameEn = nameEn;
    NameNp = nameNp;
  }
}