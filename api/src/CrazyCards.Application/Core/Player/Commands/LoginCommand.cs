using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Player.Commands;

public record LoginCommand(string Username, string Password) : ICommand<Result<LoginResponse>>;