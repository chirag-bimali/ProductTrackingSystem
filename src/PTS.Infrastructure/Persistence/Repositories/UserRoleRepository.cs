using PTS.Application.Abstractions.Persistence;
using PTS.Domain.Entities.Identities;

namespace PTS.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext db)
        : base(db) { }
}
