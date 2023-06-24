using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Cards.Queries;

internal sealed class GetCardsPaginatedHandler : IQueryHandler<GetCardsPaginatedQuery, PagedList<CardResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCardsPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<CardResponse>> Handle(GetCardsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var cardsQuery = _dbContext.Set<Card>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Class)
            .Include(i => i.Habilities)
            .Include(i => i.Image)
            .Include(i => i.Skin)
            .AsQueryable();

        cardsQuery = ApplyFilters(request, cardsQuery);

        var count = await cardsQuery.CountAsync(cancellationToken);
        
        var result = await cardsQuery
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<CardResponse>(
            _mapper.Map<List<CardResponse>>(result),
            count,
            request.Page,
            request.PageSize);
    }

    private static IQueryable<Card> ApplyFilters(GetCardsPaginatedQuery request, IQueryable<Card> cardsQuery)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
            cardsQuery = cardsQuery.Where(x => x.Name.Contains(request.Name));

        if (request.Type is not null)
            cardsQuery = cardsQuery.Where(x => x.Type == request.Type);

        if (request.Rarity is not null)
            cardsQuery = cardsQuery.Where(x => x.Rarity == request.Rarity);
        return cardsQuery;
    }
}