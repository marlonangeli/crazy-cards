using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Deck.Commands;

internal sealed class CreateBattleDeckHandler : ICommandHandler<CreateBattleDeckCommand, Result<BattleDeckResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateBattleDeckHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<BattleDeckResponse>> Handle(CreateBattleDeckCommand request, CancellationToken cancellationToken)
    {
        var battleDeck = new BattleDeck
        {
            PlayerDeckId = request.PlayerDeckId,
            HeroId = request.HeroId,
            IsDefault = request.IsDefault
        };
        
        var entity = await _dbContext.Set<BattleDeck>().AddAsync(battleDeck, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<BattleDeckResponse>(entity.Entity));
    }
}