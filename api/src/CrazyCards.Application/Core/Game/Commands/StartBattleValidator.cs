using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Game;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Game.Commands;

internal sealed class StartBattleValidator : AbstractValidator<StartBattleCommand>
{
    public StartBattleValidator(IDbContext dbContext)
    {
        RuleFor(x => x.PlayerId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Domain.Entities.Player.Player>()
                    .AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Game.PlayerDoesNotExist)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<WaitingRoom>()
                    .AsNoTracking()
                    .IgnoreAutoIncludes()
                    .OrderByDescending(o => o.CreatedAt)
                    .FirstOrDefaultAsync(x => x.IsWaiting && x.PlayerId == id, cts) is null)
            .WithError(ValidationErrors.Game.PlayesJustIsWaiting);

        RuleFor(x => x.BattleDeckId)
            .MustAsync(async (room, id, cts) =>
                await dbContext.Set<Domain.Entities.Deck.BattleDeck>()
                    .AnyAsync(x => x.Id == id && x.PlayerDeck.PlayerId == room.PlayerId, cts))
            .WithError(ValidationErrors.Game.BattleDeckDoesNotExist);
    }
}