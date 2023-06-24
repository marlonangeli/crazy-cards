using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Deck.Commands;

public record CreateBattleDeckCommand(
    Guid PlayerDeckId,
    Guid HeroId,
    bool IsDefault = false) : ICommand<Result<BattleDeckResponse>>;