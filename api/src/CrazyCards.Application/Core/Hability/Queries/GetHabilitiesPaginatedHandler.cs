using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Hability.Queries;

internal sealed class GetHabilitiesPaginatedHandler : IQueryHandler<GetHabilitiesPaginatedQuery, PagedList<HabilityResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHabilitiesPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<HabilityResponse>> Handle(GetHabilitiesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var habilities = await _dbContext.Set<Domain.Entities.Card.Hability.Hability>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Card)
            .Include(x => x.Action)
            .ThenInclude(i => i.InvokeCard)
            .OrderBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var count = await _dbContext.Set<Domain.Entities.Card.Hability.Hability>().CountAsync(cancellationToken);

        return new PagedList<HabilityResponse>(
            _mapper.Map<List<HabilityResponse>>(habilities),
            count,
            request.Page,
            request.PageSize);
    }
}