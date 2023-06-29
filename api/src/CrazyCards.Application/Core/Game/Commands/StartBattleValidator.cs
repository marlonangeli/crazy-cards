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
        RuleFor(x => x.BattleId)
            .MustAsync(async (id, cts) => await dbContext.Set<Battle>()
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .Where(x => x.Id == id && x.EndTime == null)
                .AnyAsync(cts))
            .WithError(ValidationErrors.Game.BattleDoesNotExist);
    }
}