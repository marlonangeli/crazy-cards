using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Player.Queries;

internal sealed class GetPlayerByIdHandler : IQueryHandler<GetPlayerByIdQuery, Result<PlayerResponse>>
{
    private IDbContext _dbContext;
    private IMapper _mapper;

    public GetPlayerByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<PlayerResponse>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Set<Domain.Entities.Player.Player>()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        return Result.Success(_mapper.Map<PlayerResponse>(player));
    }
}