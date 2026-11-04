using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Localization.Shared;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours;

public class ValidationCommandDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _inner;
    private readonly IEnumerable<IValidator<TCommand>> _validators;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public ValidationCommandDecorator(
        ICommandHandler<TCommand, TResult> inner,
        IEnumerable<IValidator<TCommand>> validators,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _inner = inner;
        _validators = validators;
        _localizer = localizer;
    }

    public async Task<Result<TResult>> Handle(TCommand command, CancellationToken ct)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TCommand>(command);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var details = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

                return Error.Validation(
                    "Validation.Failedfdfddf",
                    _localizer["ValidationErrors"],
                    details
                );
            }
        }

        return await _inner.Handle(command, ct);
    }
}
