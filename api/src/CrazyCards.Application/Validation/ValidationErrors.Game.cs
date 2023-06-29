using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Game
    {
        internal static Error PlayerDoesNotExist => new(
            "Game.PlayerDoesNotExist",
            "O jogador não existe");
        
        internal static Error BattleDeckDoesNotExist => new(
            "Game.BattleDeckDoesNotExist",
            "O deck de batalha não existe");
        
        internal static Error PlayesJustIsWaiting => new(
            "Game.PlayesJustIsWaiting",
            "O jogador já está em uma sala de espera");
        
        internal static Error BattleDoesNotExist => new(
            "Game.BattleDoesNotExist",
            "A batalha não existe");
    }
}