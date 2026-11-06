using PTS.Application.Abstractions.Messaging;
using PTS.Application.Abstractions.Persistence;
using PTS.Application.Abstractions.Security;
using PTS.Application.Common.Response;

namespace PTS.Application.Features.Users.Queries.GetMe;

public class GetMeQueryHandler(IUserRepository userRepository, ICurrentUser currentUser)
    : IQueryHandler<GetMeQuery, GetMeQueryResult>
{
    public async Task<Result<GetMeQueryResult>> Handle(
        GetMeQuery request,
        CancellationToken cancellationToken
    )
    {
        if (!currentUser.IsAuthenticated || currentUser.Id is null)
            return Error.Unauthorized("user.not_authenticated", "User is not authenticated.");
        var user = await userRepository.GetByIdAsync(currentUser.Id.Value, cancellationToken);

        if (user is null)
            return Error.Unauthorized("user.not_authenticated", "User is not authenticated.");

        var result = new GetMeQueryResult(user.Id, user.Email, user.NameNp, user.NameEn);

        return result;
    }
}
