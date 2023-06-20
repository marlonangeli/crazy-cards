using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Application.Core.Skin.Commands.CreateSkin;
using CrazyCards.Application.Core.Skin.Commands.Queries;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using CrazyCards.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

public class SkinController : ApiControllerBase
{
    [HttpPost]
    [Route("", Name = "CreateSkinAsync")]
    [ProducesResponseType(typeof(SkinResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSkin(
        [FromBody] CreateSkinRequest request,
        CancellationToken cancellationToken)
    {
        byte[] bytes = Convert.FromBase64String(request.Base64);
        await using Stream stream = new MemoryStream(bytes);
        
        var skinResponse = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(value => new CreateSkinCommand(
                request.Name, stream, request.MimeType))
            .Bind(command => Sender.Send(command, cancellationToken));

        return skinResponse.IsSuccess
            ? CreatedAtAction(nameof(GetSkinById), new { id = skinResponse.Value.Id }, skinResponse.Value)
            : HandleFailure(skinResponse);
    }
    
    [HttpGet]
    [Route("{id:guid}", Name = "GetSkinByIdAsync")]
    [ProducesResponseType(typeof(SkinResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSkinById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"skin:{id}";
        var cachedSkin = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Result.Success(new GetSkinByIdQuery(id))
                .Bind(query => Sender.Send(query, cancellationToken)),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return cachedSkin!.IsSuccess ? Ok(cachedSkin.Value) : HandleFailure(cachedSkin);
    }

    public SkinController(ISender sender, ILogger<SkinController> logger, IDistributedCache cache) : base(sender, logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}