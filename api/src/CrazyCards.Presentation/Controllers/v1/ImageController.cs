using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Core.Images.Commands.CreateImage;
using CrazyCards.Application.Core.Images.Queries;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

public class ImageController : ApiControllerBase
{
    [HttpPost]
    [Route("", Name = "CreateImageAsync")]
    [ProducesResponseType(typeof(ImageResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateImage(
        [FromBody] CreateImageRequest request,
        CancellationToken cancellationToken)
    {
        byte[] bytes = Convert.FromBase64String(request.Base64);
        await using Stream stream = new MemoryStream(bytes);

        var imageResponse = await Result.Success(new CreateImageCommand(stream, request.MimeType))
            .Bind(command => Sender.Send(command, cancellationToken));

        return imageResponse.IsSuccess
            ? CreatedAtAction(nameof(GetImage), new {id = imageResponse.Value.Id}, imageResponse.Value)
            : HandleFailure(imageResponse);
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetImageAsync")]
    [ProducesResponseType(typeof(Result<ImageResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetImage(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"image:{id}";
        var cachedImage = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Result.Success(new GetImageQuery(id))
                .Bind(query => Sender.Send(query, cancellationToken)),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return cachedImage!.IsSuccess ? Ok(cachedImage.Value) : HandleFailure(cachedImage);
    }

    public ImageController(ISender sender, ILogger<ImageController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }
    
    private readonly IDistributedCache _cache;
}