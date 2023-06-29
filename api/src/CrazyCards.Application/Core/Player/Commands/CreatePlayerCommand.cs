using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Player.Commands;

public record CreatePlayerCommand(string Username, string Email, string Password) : ICommand<Result<PlayerResponse>>;