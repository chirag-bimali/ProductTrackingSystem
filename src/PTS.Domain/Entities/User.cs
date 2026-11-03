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
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    public void AssignRole(Role role)
    {
        if (UserRoles.Any(ur => ur.RoleId == role.Id))
            return; // already has this role

        UserRoles.Add(new UserRole(Id, role.Id));
    }

    private User() { }

    public User(
        string nameEn,
        string nameNp,
        string contact,
        string email,
        string passwordHash,
        string? imageUrl = null
    )
    {
        Id = Guid.NewGuid();
        NameEn = nameEn;
        NameNp = nameNp;
        Contact = contact;
        Email = email;
        PasswordHash = passwordHash;
        ImageUrl = imageUrl;
        VerificationStatus = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void VerifyUser() => VerificationStatus = true;
}
