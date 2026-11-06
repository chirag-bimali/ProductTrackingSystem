using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Domain.Entities.Identities;

namespace PTS.Application.Features.Users.Commands.CreateLocalAdmin;

public class CreateLocalAdminCommandHandler(
    IPasswordHasher passwordHasher,
    IUserRepository users,
    ILocalAdminRepository localAdmins,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateLocalAdminCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateLocalAdminCommand request,
        CancellationToken cancellationToken
    )
    {
        // Create two entities: User and LocalAdmin
        var user = new User(
            request.FirstName,
            request.LastName,
            request.Contact,
            request.Email,
            passwordHasher.Hash(request.Password)
        );

        var localAdmin = new LocalAdmin
        {
            UserId = user.Id,
            MunicipalityId = request.MunicipalityId,
        };
        await users.AddAsync(user, cancellationToken);
        await localAdmins.AddAsync(localAdmin, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
