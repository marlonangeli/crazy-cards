using CrazyCards.Domain.Constants;
using CrazyCards.Domain.Primitives;

namespace CrazyCards.Application.Validation;

internal static partial class ValidationErrors
{
    internal static class Deck
    {
        internal static Error HeroDoesNotExist => new(
            "Deck.HeroDoesNotExist",
            "O herói informado não existe.");
        
        internal static Error PlayerDeckDoesNotExist => new(
            "Deck.PlayerDeckDoesNotExist",
            "O deck do jogador informado não existe.");
        
        internal static Error BattleDeckDoesNotExist => new(
            "Deck.BattleDeckDoesNotExist",
            "O deck de batalha informado não existe.");
        
        internal static Error CardDoesNotExist => new(
            "Deck.CardDoesNotExist",
            "A carta informada não existe.");
        
        internal static Error CardsIsNullOrEmpty => new(
            "Deck.CardsIsNullOrEmpty",
            "A lista de cartas não pode ser nula ou vazia.");
        
        internal static Error CardsCountExceeded => new(
            "Deck.CardsCountExceeded",
            $"A lista de cartas não pode conter mais de {DeckRules.MaximumCardsInDeck} cartas.");
    }
}