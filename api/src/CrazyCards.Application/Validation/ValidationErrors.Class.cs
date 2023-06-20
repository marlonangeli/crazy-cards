using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Class
    {
        internal static Error NameIsEmptyOrNull => new(
            "Class.NameIsEmptyOrNull",
            "O nome da classe não pode estar vazio ou nulo.");
        
        internal static Error NameIsTooLong => new(
            "Class.NameIsTooLong", 
            "O nome da classe não pode ter mais de 50 caracteres.");
        
        internal static Error DescriptionIsEmptyOrNull => new(
            "Class.DescriptionIsEmptyOrNull",
            "A descrição da classe não pode estar vazia ou nula.");
        
        internal static Error DescriptionIsTooLong => new(
            "Class.DescriptionIsTooLong",
            "A descrição da classe não pode ter mais de 512 caracteres.");
        
        internal static Error ImageDoesNotExist => new(
            "Class.ImageDoesNotExist",
            "A imagem da classe não existe.");
        
        internal static Error SkinDoesNotExist => new(
            "Class.SkinDoesNotExist",
            "A skin da classe não existe.");
    }
}