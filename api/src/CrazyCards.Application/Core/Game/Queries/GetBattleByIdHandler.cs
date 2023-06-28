using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Game.Queries;

internal sealed class GetBattleByIdHandler : IQueryHandler<GetBattleByIdQuery, Result<BattleResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBattleByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<BattleResponse>> Handle(GetBattleByIdQuery request, CancellationToken cancellationToken)
    {
        var battle = await _dbContext.Set<Battle>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Player1)
            .Include(i => i.Player2)
            .Include(i => i.Player1Deck)
            .Include(i => i.Player2Deck)
            .Include(i => i.Rounds)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return battle is null
            ? Result.Failure<BattleResponse>(
                new Error("Battle.NotFound", $"Batalha com Id {request.Id} não encontrada"))
            : Result.Success(_mapper.Map<BattleResponse>(battle));
    }
}