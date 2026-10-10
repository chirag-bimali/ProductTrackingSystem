using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PTS.Application.Abstractions.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        // Requests without a validator go straight through
        if (!_validators.Any())
            return await next();


        var results = new List<ValidationResult>();

        foreach (var validator in _validators)
        {
            var context = new ValidationContext<TRequest>(request);
            var result = await validator.ValidateAsync(context, ct);
            results.Add(result);
        }


        var failures = results
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}