using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Game.Queries;

public record GetWaitingRoomByIdQuery(Guid Id) : IQuery<Result<WaitingRoomResponse>>;