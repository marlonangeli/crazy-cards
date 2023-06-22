using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Heroes.Queries;

internal sealed class GetHeroesPaginatedHandler : IQueryHandler<GetHeroesPaginatedQuery, PagedList<HeroResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHeroesPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<HeroResponse>> Handle(GetHeroesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var heroesQuery = _dbContext.Set<Hero>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Class)
            .Include(i => i.Skin)
            .Include(i => i.Image)
            .Include(i => i.Weapon)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            heroesQuery = heroesQuery.Where(x => x.Name.Contains(request.Name));
        
        var heroes = await heroesQuery
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await _dbContext.Set<Hero>().CountAsync(cancellationToken);
        
        return new PagedList<HeroResponse>(
            _mapper.Map<List<HeroResponse>>(heroes), 
            count,
            request.Page,
            request.PageSize);
    }
}