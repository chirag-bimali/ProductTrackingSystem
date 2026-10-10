using MediatR;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Common.Abstractions.Security;
using PTS.Application.Common.Response;
using PTS.Application.Features.Authorization.Common;
using PTS.Domain.Entities;

namespace PTS.Application.Features.Authorization.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResult>>
{
    private readonly IUserRepository _users;
    private readonly IRoleRepository _roles;
    // private readonly ILocationRepository _locations;
    private readonly IPasswordHasher _hasher;
    private readonly IUnitOfWork _uow;

    public RegisterCommandHandler(
        IUserRepository users,
        IRoleRepository roles,
        // ILocationRepository locations,
        IPasswordHasher hasher,
        IUnitOfWork uow)
    {
        _users = users;
        _roles = roles;
        // _locations = locations;
        _hasher = hasher;
        _uow = uow;
    }

    public async Task<Result<RegisterResult>> Handle(RegisterCommand request, CancellationToken ct)
    {

        var role = await _roles.GetByNameAsync(request.Role, ct);
        if (role is null)
            return AuthErrors.RoleNotFound(request.Role);

        // var location = new Location(
        //     request.WardId,
        //     request.AddressLine?.Trim(),
        //     request.GpsCoordinates?.Trim());

        var user = new User(
            nameEn: request.NameEn.Trim(),
            nameNp: request.NameNp.Trim(),
            contact: request.Contact.Trim(),
            email: request.Email.Trim().ToLowerInvariant(),
            passwordHash: _hasher.Hash(request.Password),
            roleId: role.Id,
            // locationId: location.Id,
            imageUrl: request.ImageUrl);

        // await _locations.AddAsync(location, ct);
        await _users.AddAsync(user, ct);

        // One save, so the location and the user are stored together or not at all
        await _uow.SaveChangesAsync(ct);

        return new RegisterResult(user.Id, user.Email, role.NameEn, user.VerificationStatus);
    }
}