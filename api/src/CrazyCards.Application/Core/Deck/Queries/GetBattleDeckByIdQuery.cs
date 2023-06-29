using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Deck.Queries;

public record GetBattleDeckByIdQuery(Guid Id) : IQuery<Result<BattleDeckResponse>>;