using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Player.Queries;

public record GetPlayerByIdQuery(Guid Id) : IQuery<Result<PlayerResponse>>;