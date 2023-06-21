using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Player
    {
        internal static Error UserExists =>
            new("Player.UserExists", "O usuário já existe.");

        internal static Error UsernameCannotBeEmpty =>
            new("Player.UsernameCannotBeEmpty", "O nome de usuário não pode ser vazio.");

        internal static Error EmailCannotBeEmpty =>
            new("Player.EmailCannotBeEmpty", "O E-mail não pode ser vazio.");

        internal static Error InvalidEmail =>
            new("Player.InvalidEmail", "O E-mail é inválido.");

        internal static Error PasswordCannotBeEmpty =>
            new("Player.PasswordCannotBeEmpty", "A senha não pode estar vazia.");

        internal static Error UsernameExists =>
            new("Player.UsernameExists", "Nome de usuário já existe.");

        internal static Error EmailExists =>
            new("Player.EmailExists", "E-mail já existe.");
    }
}