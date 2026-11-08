using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;
using PTS.Application.Abstractions.Messaging;
using PTS.Application.Common.Localization.Shared;
using PTS.Application.Common.Response;

namespace PTS.Application.Abstractions.Behaviours.Common;

internal sealed class RequestValidation<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    IStringLocalizer<SharedResource> localizer
)
    where TRequest : IRequest<TResponse>
{
    public async Task<Error?> ValidateAsync(TRequest request, CancellationToken ct)
    {
        if (!validators.Any())
            return null;

        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, ct)));

        var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count == 0)
            return null;

        var details = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

        return Error.Validation("Validation.Failed", localizer["ValidationErrors"], details);
    }
}
