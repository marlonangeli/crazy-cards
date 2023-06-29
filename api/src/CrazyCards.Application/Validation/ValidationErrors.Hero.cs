using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Hero
    {
        internal static Error NameCannotBeNullOrEmpty => new(
            "Hero.NameCannotBeNullOrEmpty",
            "O nome náo pode ser nulo ou vazio.");
        
        internal static Error NameCannotBeLongerThan50Characters => new(
            "Hero.NameCannotBeLongerThan50Characters",
            "O nome não pode ter mais de 50 caracteres.");
        
        internal static Error DescriptionCannotBeNullOrEmpty => new(
            "Hero.DescriptionCannotBeNullOrEmpty",
            "A descrição não pode ser nula ou vazia.");
        
        internal static Error DescriptionCannotBeLongerThan512Characters => new(
            "Hero.DescriptionCannotBeLongerThan512Characters",
            "A descrição não pode ter mais de 512 caracteres.");
        
        internal static Error ImageDoesNotExist => new(
            "Hero.ImageDoesNotExist",
            "A imagem não existe.");
        
        internal static Error SkinDoesNotExist => new(
            "Hero.SkinDoesNotExist",
            "A skin não existe.");
        
        internal static Error ClassDoesNotExist => new(
            "Hero.ClassDoesNotExist",
            "A classe não existe.");
        
        internal static Error WeaponDoesNotExist => new(
            "Hero.WeaponDoesNotExist",
            "A arma não existe.");
    }
}