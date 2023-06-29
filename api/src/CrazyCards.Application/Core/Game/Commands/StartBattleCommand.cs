﻿using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Game.Commands;

public record StartBattleCommand(Guid BattleId) : ICommand<Result<BattleResponse>>;