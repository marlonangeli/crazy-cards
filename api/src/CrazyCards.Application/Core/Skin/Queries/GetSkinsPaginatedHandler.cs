using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Skin.Queries;

internal sealed class GetSkinsPaginatedHandler : IQueryHandler<GetSkinsPaginatedQuery, PagedList<SkinResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSkinsPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<SkinResponse>> Handle(GetSkinsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var skins = await _dbContext.Set<Domain.Entities.Card.Skin>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await _dbContext.Set<Domain.Entities.Card.Skin>().CountAsync(cancellationToken);
        
        return new PagedList<SkinResponse>(
            _mapper.Map<List<SkinResponse>>(skins),
            count,
            request.Page,
            request.PageSize);
    }
}