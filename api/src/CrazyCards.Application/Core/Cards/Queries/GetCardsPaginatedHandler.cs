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

    public async Task<PagedList<CardResponse>> Handle(GetCardsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var cards = _dbContext.Set<Card>()
            .AsNoTracking()
            .Include(i => i.Class)
            .Include(i => i.Habilities)
            .Include(i => i.Image)
            .Include(i => i.Skin)
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);
        
        var count = await _dbContext.Set<Card>().CountAsync(cancellationToken);

        var result = await _mapper.ProjectTo<CardResponse>(cards).ToListAsync(cancellationToken);
        return new PagedList<CardResponse>(result, count, request.Page, request.PageSize);
    }
}