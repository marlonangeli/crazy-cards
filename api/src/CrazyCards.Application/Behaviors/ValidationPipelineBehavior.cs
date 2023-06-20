using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Domain.Primitives.ValidationResult;
using FluentValidation;
using MediatR;
using ValidationResult = CrazyCards.Domain.Primitives.ValidationResult.ValidationResult;

namespace CrazyCards.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(async validator => await validator.ValidateAsync(request, cancellationToken))
            .SelectMany(validatorResult => validatorResult.Result.Errors)
            .Where(failure => failure is not null)
            .Select(error => new Error(
                error.PropertyName, error.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any()) return CreateValidationResult<TResponse>(errors);
        
        return await next();
    }
    
    private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;
        
        return (TResult)validationResult!;
    }
}