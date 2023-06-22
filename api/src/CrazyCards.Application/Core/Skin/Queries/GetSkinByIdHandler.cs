using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Application.Core.Skin.Commands.Queries;
using CrazyCards.Application.Interfaces;
using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Skin.Queries;

internal sealed class GetSkinByIdHandler : IQueryHandler<GetSkinByIdQuery, Result<SkinResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IBlobStorageService _blobStorageService;

    public GetSkinByIdHandler(IDbContext dbContext, IBlobStorageService blobStorageService)
    {
        _dbContext = dbContext;
        _blobStorageService = blobStorageService;
    }

    public async Task<Result<SkinResponse>> Handle(GetSkinByIdQuery request, CancellationToken cancellationToken)
    {
        var skin = await _dbContext.Set<Domain.Entities.Card.Skin>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (skin is null)
        {
            return (Result<SkinResponse>)Result.Failure(new Error("SkinNotFound", "Skin não encontrada"));
        }

        var url = await _blobStorageService.GetUrlAsync(skin.Id, cancellationToken);

        return Result.Success(new SkinResponse
        {
            Id = skin.Id,
            Size = skin.Size,
            MimeType = skin.MimeType,
            Name = skin.Name,
            Url = url
        });
    }
}