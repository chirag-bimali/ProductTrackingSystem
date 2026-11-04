using FluentValidation;
using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Localization.Shared;
using PTS.Application.Common.Response;

public class ValidationQueryDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
{
    private readonly IQueryHandler<TQuery, TResult> _inner;
    private readonly IEnumerable<IValidator<TQuery>> _validators;

    private readonly IStringLocalizer<SharedResource> _localizer;

    public ValidationQueryDecorator(
        IQueryHandler<TQuery, TResult> inner,
        IEnumerable<IValidator<TQuery>> validators,
        IStringLocalizer<SharedResource> localizer
    )
    {
        _inner = inner;
        _validators = validators;
        _localizer = localizer;
    }

    public async Task<Result<TResult>> Handle(TQuery command, CancellationToken ct)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TQuery>(command);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                var details = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

                return Error.Validation(
                    "Validation.Failed",
                    _localizer["ValidationErrors"],
                    details
                );
            }
        }

        return await _inner.Handle(command, ct);
    }
}
