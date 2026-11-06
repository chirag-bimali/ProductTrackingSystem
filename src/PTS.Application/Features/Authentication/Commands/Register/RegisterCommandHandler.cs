using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Abstractions.Security;
using PTS.Application.Common.Localization.Roles;
using PTS.Application.Common.Response;
using PTS.Domain.Entities.Identities;

namespace PTS.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
{
    private readonly IUserRepository _users;
    private readonly IUserRoleRepository _userRoles;
    private readonly IRoleRepository _roles;
    private readonly IStringLocalizer<RoleResource> _localizer;

    // private readonly ILocationRepository _locations;
    private readonly IPasswordHasher _hasher;

    private readonly IUnitOfWork _uow;

    public RegisterCommandHandler(
        IStringLocalizer<RoleResource> localizer,
        IUserRepository users,
        IRoleRepository roles,
        // ILocationRepository locations,
        IUserRoleRepository userRoles,
        IPasswordHasher hasher,
        IUnitOfWork uow
    )
    {
        _users = users;
        _roles = roles;
        _userRoles = userRoles;
        _hasher = hasher;
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken ct)
    {
        var role = await _roles.GetByNameAsync(request.Role, ct);
        if (role is null)
            return Error.NotFound("role.not_found", _localizer["RoleNotFound", request.Role]);

        var user = new User(
            nameEn: request.NameEn.Trim(),
            nameNp: request.NameNp.Trim(),
            contact: request.Contact.Trim(),
            email: request.Email.Trim().ToLowerInvariant(),
            passwordHash: _hasher.Hash(request.Password)
        );

        var userRole = new UserRole(user.Id, role.Id);

        await _users.AddAsync(user, ct);
        await _userRoles.AddAsync(userRole, ct);

        await _uow.SaveChangesAsync(ct);

        return user.Id;
    }
}
