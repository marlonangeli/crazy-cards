using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Images.Queries;

internal sealed class GetImageHandler : IQueryHandler<GetImageQuery, Result<ImageResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;

    public GetImageHandler(IDbContext dbContext, IBlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<ImageResponse>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var image = await _dbContext.Set<Image>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (image is null)
        {
            return Result.Failure<ImageResponse>(new Error("Image.NotFound", "Imagem não encontrada"));
        }
        
        var url = await _blobStorageService.GetUrlAsync(image.Id, cancellationToken);

        return Result.Success(new ImageResponse
        {
            Id = image.Id,
            Size = image.Size,
            MimeType = image.MimeType,
            Url = url
        });
    }
}