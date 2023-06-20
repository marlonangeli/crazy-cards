namespace CrazyCards.Domain.Primitives.ValidationResult;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "ValidationError",
        "Ocorreu um erro de validação.");
    
    Error[] Errors { get; }
}