using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Game.Commands;

internal sealed class StartBattleHandler : ICommandHandler<StartBattleCommand, Result<WaitingRoomResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public StartBattleHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<WaitingRoomResponse>> Handle(StartBattleCommand request, CancellationToken cancellationToken)
    {
        var waitingRoom = await _dbContext.Set<WaitingRoom>()
            .IgnoreAutoIncludes()
            .Include(i => i.Player)
            .Include(i => i.BattleDeck)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(x => x.IsWaiting, cancellationToken);

        var entity = new WaitingRoom
        {
            PlayerId = request.PlayerId,
            BattleDeckId = request.BattleDeckId,
            IsWaiting = true
        };

        if (waitingRoom is null)
        {
            _dbContext.Insert(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(_mapper.Map<WaitingRoomResponse>(entity));
        }

        var id = Guid.NewGuid();
        var battle = new Battle
        {
            Id = id,
            Player1Id = waitingRoom.PlayerId,
            Player1DeckId = waitingRoom.BattleDeckId,
            Player2Id = request.PlayerId,
            Player2DeckId = request.BattleDeckId
        };
        
        _dbContext.Insert(battle);
        
        waitingRoom.IsWaiting = false;
        waitingRoom.BattleId = id;
        _dbContext.Set<WaitingRoom>().Update(waitingRoom);
        
        entity.IsWaiting = false;
        entity.BattleId = id;
        _dbContext.Insert(entity);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(_mapper.Map<WaitingRoomResponse>(entity));
    }
}