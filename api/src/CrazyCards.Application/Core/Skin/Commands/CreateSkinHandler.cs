using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Application.Core.Skin.Commands;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Skin.CreateSkin;

internal sealed class CreateSkinHandler : ICommandHandler<CreateSkinCommand, Result<SkinResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;

    public CreateSkinHandler(IDbContext dbContext, IBlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<SkinResponse>> Handle(CreateSkinCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var size = (int)request.Stream.Length;
        var mimeType = request.MimeType;

        var saveSkinToDatabaseResult = await SaveSkinToDatabase(id, size, mimeType, request.Name, cancellationToken);
        if (saveSkinToDatabaseResult.IsFailure)
        {
            return (Result<SkinResponse>)Result.Failure(saveSkinToDatabaseResult.Error);
        }

        var saveSkinToBlobStorageResult =
            await SaveImageToBlobStorage(id, request.Stream, request.MimeType, cancellationToken);
        if (saveSkinToBlobStorageResult.IsFailure)
        {
            return (Result<SkinResponse>)Result.Failure(saveSkinToBlobStorageResult.Error);
        }

        return Result.Success(new SkinResponse()
        {
            Id = id,
            Size = size,
            MimeType = mimeType,
            Url = saveSkinToBlobStorageResult.Value,
            Name = request.Name
        });
    }

    private async Task<Result> SaveSkinToDatabase(Guid id, int size, string mimeType, string name,
        CancellationToken cancellationToken)
    {
        var skin = new Domain.Entities.Card.Skin
        {
            Id = id,
            Size = size,
            MimeType = mimeType,
            Name = name
        };

        _dbContext.Insert(skin);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
    
    // TODO - remver duplicidade
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