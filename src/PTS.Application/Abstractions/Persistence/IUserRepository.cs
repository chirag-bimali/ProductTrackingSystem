using PTS.Domain.Entities;

namespace PTS.Application.Abstractions.Persistence;

public interface IUserRepository : IRepository<User>
{
    // Finds a user by email OR contact number. Includes the Role.
    Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetByContactAsync(string contact, CancellationToken ct = default);

    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    Task<bool> ContactExistsAsync(string contact, CancellationToken ct = default);
}
