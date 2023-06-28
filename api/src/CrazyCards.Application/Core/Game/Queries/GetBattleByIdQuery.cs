using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Game.Queries;

public record GetBattleByIdQuery(Guid Id) : IQuery<Result<BattleResponse>>;