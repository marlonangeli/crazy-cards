using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Core.Cards.Commands.CreateCard;
using CrazyCards.Application.Core.Cards.Queries;
using CrazyCards.Domain.Enum;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

public class CardController : ApiControllerBase
{
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(CardResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCard(
        [FromBody] CreateCardRequest request,
        CancellationToken cancellationToken)
    {
        var cardResponse = await Result.Success(new CreateCardCommand(
                request.ManaCost,
                request.Name,
                request.Description,
                request.ImageId,
                request.SkinId,
                request.ClassId,
                Rarity.FromValue(request.Rarity),
                CardType.FromValue(request.Type)))
            .Bind(command => Sender.Send(command, cancellationToken));

        return cardResponse.IsSuccess
            ? CreatedAtAction(nameof(GetCardById), new { id = cardResponse.Value.Id }, cardResponse.Value)
            : HandleFailure(cardResponse);
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(PagedList<CardResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GatCardsPaginated(
        [FromQuery] GetCardsPaginatedRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"cards:{request.Page}:{request.PageSize}";

        var result = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () =>
                Sender.Send(new GetCardsPaginatedQuery(request.Page, request.PageSize), cancellationToken),
            TimeSpan.FromMinutes(1));

        if (result is null)
        {
            return NoContent();
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetCardByIdAsync")]
    [ProducesResponseType(typeof(CardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"card:{id}";

        var result = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetCardByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(1), cancellationToken: cancellationToken);

        return result!.IsSuccess
            ? Ok(result.Value)
            : NotFound(result);
    }
    
    public CardController(ISender sender, ILogger<CardController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }
    
    private readonly IDistributedCache _cache;
}
