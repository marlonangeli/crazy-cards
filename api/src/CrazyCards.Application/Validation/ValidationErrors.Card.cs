using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Card
    {
        internal static Error ManaCostIsNegative => new(
            "Card.ManaCostIsNegative",
            "O custo de mana não pode ser negativo.");
        
        internal static Error NameIsNullOrEmpty => new(
            "Card.NameIsNullOrEmpty",
            "O nome não pode ser nulo ou vazio.");
        
        internal static Error NameIsTooLong => new(
            "Card.NameIsTooLong",
            "O nome não pode ter mais de 100 caracteres.");
        
        internal static Error DescriptionIsNullOrEmpty => new(
            "Card.DescriptionIsNullOrEmpty",
            "A descrição não pode ser nula ou vazia.");
        
        internal static Error DescriptionIsTooLong => new(
            "Card.DescriptionIsTooLong",
            "A descrição não pode ter mais de 512 caracteres.");

        internal static Error ClassDoesNotExist => new(
            "Card.ClassDoesNotExist",
            "A classe não existe.");
        
        internal static Error ImageDoesNotExist => new(
            "Card.ImageDoesNotExist",
            "A imagem não existe.");
        
        internal static Error SkinDoesNotExist => new(
            "Card.SkinDoesNotExist",
            "A skin não existe.");
        
        internal static Error RarityDoesNotExist => new(
            "Card.RarityDoesNotExist",
            "A raridade não existe.");
        
        internal static Error TypeDoesNotExist => new(
            "Card.TypeDoesNotExist",
            "O tipo não existe.");
    }
}