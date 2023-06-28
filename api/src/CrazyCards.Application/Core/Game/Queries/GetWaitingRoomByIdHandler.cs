using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Game;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Game.Queries;

internal sealed class GetWaitingRoomByIdHandler : IQueryHandler<GetWaitingRoomByIdQuery, Result<WaitingRoomResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetWaitingRoomByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<WaitingRoomResponse>> Handle(GetWaitingRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var battle = await _dbContext.Set<WaitingRoom>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Player)
            .Include(i => i.Battle)
            .Include(i => i.BattleDeck)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return battle is null
            ? Result.Failure<WaitingRoomResponse>(
                new Error("WaitingRoom.NotFound", $"Sala de espera com Id {request.Id} não encontrada"))
            : Result.Success(_mapper.Map<WaitingRoomResponse>(battle));
    }
}