namespace CrazyCards.Domain.Primitives.ValidationResult;

public sealed class ValidationResult : Result.Result, IValidationResult
{
    internal ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }
    
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}