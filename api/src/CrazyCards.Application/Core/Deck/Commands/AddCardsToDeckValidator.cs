using CrazyCards.Application.Extensions;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Validation;
using CrazyCards.Domain.Constants;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Deck;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Deck.Commands;

internal sealed class AddCardsToDeckValidator : AbstractValidator<AddCardsToDeckCommand>
{
    public AddCardsToDeckValidator(IDbContext dbContext)
    {
        RuleFor(x => x.BattleDeckId)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<BattleDeck>().AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Deck.BattleDeckDoesNotExist);

        RuleForEach(x => x.CardIds)
            .MustAsync(async (id, cts) =>
                await dbContext.Set<Card>().AnyAsync(x => x.Id == id, cts))
            .WithError(ValidationErrors.Deck.CardDoesNotExist);

        RuleFor(x => x.CardIds)
            .NotNull()
            .NotEmpty()
            .WithError(ValidationErrors.Deck.CardsIsNullOrEmpty)
            .MustAsync(async (request, cards, cts) =>
            {
                var cardsInDeck = await dbContext.Set<BattleDeck>()
                    .AsNoTracking()
                    .IgnoreAutoIncludes()
                    .Include(i => i.Cards)
                    .Where(w => w.Id == request.BattleDeckId)
                    .SelectMany(d => d.Cards)
                    .Select(c => c.Id)
                    .ToListAsync(cts);

                var cardsToAdd = cards.Except(cardsInDeck).Distinct().ToList();

                return cardsToAdd.Count + cardsInDeck.Count <= DeckRules.MaximumCardsInDeck;
            })
            .WithError(ValidationErrors.Deck.CardsCountExceeded);
    }
}