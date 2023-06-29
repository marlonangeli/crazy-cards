using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Deck.Queries;

internal sealed class GetBattleDeckByIdHandler : IQueryHandler<GetBattleDeckByIdQuery, Result<BattleDeckResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBattleDeckByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<BattleDeckResponse>> Handle(GetBattleDeckByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<BattleDeck>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.PlayerDeck)
            .Include(i => i.Hero)
            .Include(i => i.Cards)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
        
        if (entity is null)
        {
            return Result.Failure<BattleDeckResponse>(new Error(
                "BattleDeck.NotFound", "Deck de batalha não encontrado."));
        }

        return Result.Success(_mapper.Map<BattleDeckResponse>(entity));
    }
}