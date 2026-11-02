using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResult>
{
    Task<Result<TResult>> Handle(TQuery query, CancellationToken ct);
}