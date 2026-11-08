using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Messaging;

public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken ct);
}
