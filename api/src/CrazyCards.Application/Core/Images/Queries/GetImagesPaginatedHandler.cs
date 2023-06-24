using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Core.Shared;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Images.Queries;

internal sealed class GetImagesPaginatedHandler : IQueryHandler<GetImagesPaginatedQuery, PagedList<ImageResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IMapper _mapper;

    public GetImagesPaginatedHandler(IDbContext dbContext, IBlobStorageService blobStorageService, IMapper mapper)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
        _mapper = mapper;
    }

    public async Task<PagedList<ImageResponse>> Handle(GetImagesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var images = await _dbContext.Set<Image>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await _dbContext.Set<Image>().CountAsync(cancellationToken);
        
        var result = _mapper.Map<List<ImageResponse>>(images);

        var uri = _blobStorageService.GetContainerUri();
        foreach (var image in result)
        {
            image.Url = $"{uri}/{image.Id}{image.MimeType.GetExtensionFile()}";
        }
        
        return new PagedList<ImageResponse>(
            result,
            count,
            request.Page,
            request.PageSize);
    }
}