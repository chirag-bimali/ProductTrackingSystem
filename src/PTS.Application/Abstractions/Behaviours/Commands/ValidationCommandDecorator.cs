using PTS.Application.Abstractions.Behaviours.Common;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours.Commands;

internal sealed class ValidationCommandDecorator<TCommand, TResponse>(
    ICommandHandler<TCommand, TResponse> inner,
    RequestValidation<TCommand, TResponse> validation
) : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct)
    {
        var error = await validation.ValidateAsync(command, ct);

        if (error is not null)
            return error;

        return await inner.Handle(command, ct);
    }
}
