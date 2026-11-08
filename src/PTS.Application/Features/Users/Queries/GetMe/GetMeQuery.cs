using PTS.Application.Abstractions.Messaging;

namespace PTS.Application.Features.Users.Queries.GetMe;

public record GetMeQuery() : IQuery<GetMeQueryResult>;
