using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Game.Commands;

internal sealed class StartBattleHandler : ICommandHandler<StartBattleCommand, Result<BattleResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public StartBattleHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<BattleResponse>> Handle(StartBattleCommand request, CancellationToken cancellationToken)
    {
        // TODO - Mudar para um background job
        
        var battle = await _dbContext.Set<Battle>()
            .IgnoreAutoIncludes()
            .Include(i => i.Player1)
            .Include(i => i.Player2)
            .Include(i => i.Player1Deck)
            .Include(i => i.Player2Deck)
            .FirstOrDefaultAsync(x => x.Id == request.BattleId, cancellationToken);

        var gameCards1 = await CreateGameCards(request, battle!.Player1DeckId, cancellationToken);
        var gameCards2 = await CreateGameCards(request, battle!.Player2DeckId, cancellationToken);

        await _dbContext.BulkInsertEntitiesAsync(gameCards1, cancellationToken);
        await _dbContext.BulkInsertEntitiesAsync(gameCards2, cancellationToken);
        
        await _dbContext.BulkSaveAsync(cancellationToken);

        return _mapper.Map<BattleResponse>(battle);
    }

    private async Task<List<GameCard>> CreateGameCards(StartBattleCommand request, Guid battleDeckId,
        CancellationToken cancellationToken)
    {
        var player1Cards = await _dbContext.Set<BattleDeck>()
            .IgnoreAutoIncludes()
            .Where(x => x.Id == battleDeckId)
            .SelectMany(s => s.Cards)
            .Include(i => i.Class)
            .Include(i => i.Image)
            .Include(i => i.Skin)
            .Include(i => i.Habilities)
            .ToListAsync(cancellationToken);

        var gameDeck1 = await _dbContext.Set<GameDeck>().AddAsync(new GameDeck
        {
            BattleId = request.BattleId
        }, cancellationToken);

        List<GameCard> gameCards = new();
        foreach (var card in player1Cards)
        {
            var current = new GameCardAttributeProperty();
            current.ManaCost = card.ManaCost;

            switch (card)
            {
                case MinionCard minionCard:
                    current.Attack = minionCard.Attack;
                    current.Health = minionCard.Health;
                    break;

                case SpellCard spellCard:
                    current.Damage = spellCard.Damage;
                    current.Heal = spellCard.Heal;
                    break;

                case WeaponCard weaponCard:
                    current.Durability = weaponCard.Durability;
                    current.Damage = weaponCard.Damage;
                    current.Shield = weaponCard.Shield;
                    break;

                case TotenCard totenCard:
                    current.Heal = totenCard.Heal;
                    current.Shield = totenCard.Shield;
                    break;
            }

            gameCards.Add(new GameCard
            {
                GameDeckId = gameDeck1.Entity.Id,
                OriginalCardId = card.Id,
                At = new GameCardPositionProperty
                {
                    Deck = true,
                    Graveyard = false,
                    Hand = false,
                    Table = false
                },
                Is = new GameCardStatusProperty(),
                Current = current
            });
        }

        return gameCards;
    }
}