namespace PTS.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string NameEn { get; private set; } = string.Empty;
    public string NameNp { get; private set; } = string.Empty;
    public string Contact { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? ImageUrl { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public bool VerificationStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    private User() { }

    public User(
        string nameEn,
        string nameNp,
        string contact,
        string email,
        string passwordHash,
        Guid roleId,
        // Guid locationId,
        string? imageUrl = null)
    {
        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        Contact = contact;
        Email = email;
        PasswordHash = passwordHash;
        RoleId = roleId;
        ImageUrl = imageUrl;
        VerificationStatus = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void VerifyUser() => VerificationStatus = true;
}
