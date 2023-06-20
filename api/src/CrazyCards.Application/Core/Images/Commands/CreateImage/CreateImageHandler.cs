using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Images.Commands.CreateImage;

internal sealed class CreateImageHandler : ICommandHandler<CreateImageCommand, Result<ImageResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;

    public CreateImageHandler(IDbContext dbContext, IBlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<ImageResponse>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var size = (int)request.Stream.Length;
        var mimeType = request.MimeType;

        var saveImageToDatabaseResult = await SaveImageToDatabase(id, size, mimeType, cancellationToken);
        if (saveImageToDatabaseResult.IsFailure)
        {
            return (Result<ImageResponse>)Result.Failure(saveImageToDatabaseResult.Error);
        }

        var saveImageToBlobStorageResult =
            await SaveImageToBlobStorage(id, request.Stream, request.MimeType, cancellationToken);
        if (saveImageToBlobStorageResult.IsFailure)
        {
            return (Result<ImageResponse>)Result.Failure(saveImageToBlobStorageResult.Error);
        }

        return Result.Success(new ImageResponse
        {
            Id = id,
            Size = size,
            MimeType = mimeType,
            Url = saveImageToBlobStorageResult.Value
        });
    }

    private async Task<Result> SaveImageToDatabase(Guid id, int size, string mimeType,
        CancellationToken cancellationToken)
    {
        var image = new Image
        {
            Id = id,
            Size = size,
            MimeType = mimeType
        };

        _dbContext.Insert(image);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private async Task<Result<string>> SaveImageToBlobStorage(Guid id, Stream content, string mimeType,
        CancellationToken cancellationToken)
    {
        var extensionFile = mimeType switch
        {
            "image/svg+xml" => ".svg",
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, null)
        };
        var uri = await _blobStorageService.UploadAsync(id, extensionFile, content, cancellationToken);

        return Result.Success(uri);
    }
}