using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Application.Core.Deck.Commands;
using CrazyCards.Application.Core.Deck.Queries;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using CrazyCards.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

/// <summary>
/// Decks
/// </summary>
public class DeckController : ApiControllerBase
{
    /// <summary>
    /// Criar um novo deck
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("", Name = "CreateBattleDeckAsync")]
    [ProducesResponseType(typeof(BattleDeckResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBattleDeck(
        [FromBody] CreateBattleDeckRequest request,
        CancellationToken cancellationToken)
    {
        var battleDeckRespose = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(_ => new CreateBattleDeckCommand(
                request.PlayerDeckId,
                request.HeroId,
                request.IsDefault))
            .Bind(command => Sender.Send(command, cancellationToken));
        
        return battleDeckRespose.IsSuccess
            ? Ok(battleDeckRespose.Value)
            : HandleFailure(battleDeckRespose);
    }
    
    /// <summary>
    /// Obter um deck pelo seu identificador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}", Name = "GetBattleDeckByIdAsync")]
    [ProducesResponseType(typeof(BattleDeckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBattleDeckById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"battledeck:{id}";
        
        var battleDeck = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetBattleDeckByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(5),
            cancellationToken);
        
        return battleDeck is { IsSuccess: true }
            ? Ok(battleDeck.Value)
            : HandleFailure(battleDeck);
    }

    /// <summary>
    /// Adicionar cartas a um deck
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id:guid}/cards", Name = "AddCardsToBattleDeckAsync")]
    [ProducesResponseType(typeof(BattleDeckResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCardsToBattleDeck(
        [FromBody] AddCardsToBattleDeckRequest request,
        CancellationToken cancellationToken)
    {
            var battleDeckRespose = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(_ => new AddCardsToDeckCommand(
                request.BattleDeckId,
                request.CardIds.Distinct().ToList()))
            .Bind(command => Sender.Send(command, cancellationToken));
        
        return battleDeckRespose.IsSuccess
            ? Ok(battleDeckRespose.Value)
            : HandleFailure(battleDeckRespose);
    }

    /// <inheritdoc />
    public DeckController(ISender sender, ILogger<DeckController> logger, IDistributedCache cache) : base(sender, logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}