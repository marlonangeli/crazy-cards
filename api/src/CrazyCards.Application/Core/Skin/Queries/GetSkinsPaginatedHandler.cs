using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Application.Core.Shared;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Skin.Queries;

internal sealed class GetSkinsPaginatedHandler : IQueryHandler<GetSkinsPaginatedQuery, PagedList<SkinResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IMapper _mapper;

    public GetSkinsPaginatedHandler(IDbContext dbContext, IBlobStorageService blobStorageService, IMapper mapper)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
        _mapper = mapper;
    }

    public async Task<PagedList<SkinResponse>> Handle(GetSkinsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var skins = await _dbContext.Set<Domain.Entities.Card.Skin>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .OrderBy(x => x.Id)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await _dbContext.Set<Domain.Entities.Card.Skin>().CountAsync(cancellationToken);

        var result = _mapper.Map<List<SkinResponse>>(skins);
        
        var uri = _blobStorageService.GetContainerUri();
        foreach (var skin in result)
        {
            skin.Url = $"{uri}/{skin.Id}{skin.MimeType.GetExtensionFile()}";
        }
        
        return new PagedList<SkinResponse>(
            result,
            count,
            request.Page,
            request.PageSize);
    }
}