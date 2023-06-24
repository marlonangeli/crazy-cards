using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Deck;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Deck.Commands;

internal sealed class CreateBattleDeckValidator : AbstractValidator<CreateBattleDeckCommand>
{
    public CreateBattleDeckValidator(IDbContext dbContext)
    {
        RuleFor(x => x.HeroId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Hero>().AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Deck.HeroDoesNotExist);
        
        RuleFor(x => x.PlayerDeckId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<PlayerDeck>().AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Deck.PlayerDeckDoesNotExist);
    }
}