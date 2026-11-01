using FluentValidation;
using MediatR;
using ProductCatalog.Application.Common.Models;

namespace ProductCatalog.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = results
            .SelectMany(r => r.Errors)
            .Select(f => $"{f.PropertyName.Split('.').Last()}: {f.ErrorMessage}")
            .ToArray();

        if (errors.Length > 0)
            return new TResponse { Errors = errors };

        return await next();
    }
}
