using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Core.Cards.Commands;
using CrazyCards.Application.Core.Cards.Queries;
using CrazyCards.Domain.Enum;
using CrazyCards.Domain.Enum.Shared;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

/// <summary>
/// Cartas
/// </summary>
public class CardController : ApiControllerBase
{
    /// <summary>
    /// Criar uma nova carta
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("", Name = "CreateCardAsync")]
    [ProducesResponseType(typeof(CardResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                CardType.FromValue(request.Type),
                request.AdditionalProperties))
            .Bind(command => Sender.Send(command, cancellationToken));

        return cardResponse.IsSuccess
            ? CreatedAtAction(nameof(GetCardById), new { id = cardResponse.Value.Id }, cardResponse.Value)
            : HandleFailure(cardResponse);
    }

    /// <summary>
    /// Obter cartas de forma paginada
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("", Name = "GetCardsAsync")]
    [ProducesResponseType(typeof(PagedList<CardResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GatCardsPaginated(
        [FromQuery] GetCardsRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"cards:{request.Page}:{request.PageSize}:{request.Name}:{request.Type}:{request.Rarity}";

        var cards = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () =>
                Sender.Send(new GetCardsPaginatedQuery(
                    (int)request.Page,
                    (int)request.PageSize,
                    request.Name,
                    CardType.FromValue(request.Type ?? 0),
                    Rarity.FromValue(request.Rarity ?? 0)), cancellationToken),
            TimeSpan.FromMinutes(1), cancellationToken);

        if (cards is null)
        {
            return NoContent();
        }

        return Ok(cards);
    }

    /// <summary>
    /// Obter uma carta por id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}", Name = "GetCardByIdAsync")]
    [ProducesResponseType(typeof(CardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCardById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"card:{id}";

        var card = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetCardByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(1), cancellationToken: cancellationToken);

        return card!.IsSuccess
            ? Ok(card.Value)
            : NotFound(card);
    }
    
    /// <summary>
    /// Obter os tipos de cartas
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("types", Name = "GetCardTypesAsync")]
    [ProducesResponseType(typeof(IEnumerable<CardType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCardTypes()
        => Ok(Enumeration<CardType>.All);

    /// <summary>
    /// Obter os tipos de raridade
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("rarities", Name = "GetRaritiesAsync")]
    [ProducesResponseType(typeof(IEnumerable<Rarity>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetRarities() 
        => Ok(Enumeration<Rarity>.All);

    /// <inheritdoc />
    public CardController(ISender sender, ILogger<CardController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}