using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Deck.Commands;

internal sealed class AddCardsToDeckHandler : ICommandHandler<AddCardsToDeckCommand, Result<BattleDeckResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public AddCardsToDeckHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<BattleDeckResponse>> Handle(AddCardsToDeckCommand request, CancellationToken cancellationToken)
    {
        var battleDeck = await _dbContext.Set<BattleDeck>()
            .Include(i => i.Cards)
            .FirstOrDefaultAsync(x => x.Id == request.BattleDeckId, cancellationToken);

        var cards = await _dbContext.Set<Card>()
            .IgnoreAutoIncludes()
            .Where(x => request.CardIds.Contains(x.Id))
            .ToListAsync(cancellationToken);
        
        foreach (var cardId in request.CardIds)
        {
            if (battleDeck!.Cards.All(x => x.Id != cardId))
            {
                battleDeck.Cards.Add(cards.Single(x => x.Id == cardId));
            }
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<BattleDeckResponse>(battleDeck));
    }
}