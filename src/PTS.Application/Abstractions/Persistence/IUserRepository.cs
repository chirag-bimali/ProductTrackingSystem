using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface IUserRepository
{
    // Finds a user by email OR contact number. Includes the Role.
    Task<User?> GetByIdentifierAsync(string identifier, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    Task<bool> ContactExistsAsync(string contact, CancellationToken ct = default);

    Task AddAsync(User user, CancellationToken ct = default);
}