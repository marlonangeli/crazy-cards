using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Core.Shared;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Images.Queries;

internal sealed class GetImageByIdHandler : IQueryHandler<GetImageByIdQuery, Result<ImageResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;

    public GetImageByIdHandler(IDbContext dbContext, IBlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<ImageResponse>> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await _dbContext.Set<Image>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (image is null)
        {
            return Result.Failure<ImageResponse>(new Error("Image.NotFound", "Imagem não encontrada"));
        }

        var url =
            $"{_blobStorageService.GetContainerUri()}/{image.Id}{image.MimeType.GetExtensionFile()}";

        return Result.Success(new ImageResponse
        {
            Id = image.Id,
            Size = image.Size,
            MimeType = image.MimeType,
            Url = url
        });
    }
}