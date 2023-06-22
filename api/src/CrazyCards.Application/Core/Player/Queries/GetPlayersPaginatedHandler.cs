using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Player.Queries;

internal sealed class GetPlayersPaginatedHandler : IQueryHandler<GetPlayersPaginatedQuery, PagedList<PlayerResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPlayersPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<PlayerResponse>> Handle(GetPlayersPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var playerQuery = _dbContext.Set<Domain.Entities.Player.Player>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .AsQueryable();

        playerQuery = UseFilters(request, playerQuery);

        var players = await playerQuery
            .OrderBy(x => x.Username)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var count = await _dbContext.Set<Domain.Entities.Player.Player>().CountAsync(cancellationToken);
        
        return new PagedList<PlayerResponse>(
            _mapper.Map<List<PlayerResponse>>(players),
            count,
            request.Page,
            request.PageSize);
    }

    private static IQueryable<Domain.Entities.Player.Player> UseFilters(
        GetPlayersPaginatedQuery request,
        IQueryable<Domain.Entities.Player.Player> playerQuery)
    {
        if (!string.IsNullOrWhiteSpace(request.Username))
            playerQuery = playerQuery.Where(x => x.Username.Contains(request.Username));

        if (!string.IsNullOrWhiteSpace(request.Email))
            playerQuery = playerQuery.Where(x => x.Email.Contains(request.Email));
        
        return playerQuery;
    }
}