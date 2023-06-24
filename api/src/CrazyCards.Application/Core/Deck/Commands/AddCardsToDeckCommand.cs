using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Deck.Commands;

public record AddCardsToDeckCommand(
    Guid BattleDeckId,
    ICollection<Guid> CardIds) : ICommand<Result<BattleDeckResponse>>;