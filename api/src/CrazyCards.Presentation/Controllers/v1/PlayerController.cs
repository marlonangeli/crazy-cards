using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Core.Player.Commands.CreatePlayer;
using CrazyCards.Application.Core.Player.Queries;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Infrastructure.Cache;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers.v1;

/// <summary>
/// Jogador
/// </summary>
public class PlayerController : ApiControllerBase
{
    [HttpPost]
    [Route("", Name = "CreatePlayerAsync")]
    [ProducesResponseType(typeof(PlayerResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePlayer(
        [FromBody] CreatePlayerRequest request,
        CancellationToken cancellationToken)
    {
        var playerResponse = await Result.Success(new CreatePlayerCommand(
                request.Username,
                request.Email,
                request.Password))
            .Bind(command => Sender.Send(command, cancellationToken));

        return playerResponse.IsSuccess
            ? CreatedAtAction(nameof(GetPlayerById), new { id = playerResponse.Value.Id }, playerResponse.Value)
            : HandleFailure(playerResponse);
    }
    
    [HttpGet]
    [Route("{id:guid}", Name = "GetPlayerByIdAsync")]
    [ProducesResponseType(typeof(PlayerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayerById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"player:{id}";
        var cachedPlayer = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Result.Success(new GetPlayerByIdQuery(id))
                .Bind(query => Sender.Send(query, cancellationToken)),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return cachedPlayer!.IsSuccess ? Ok(cachedPlayer.Value) : HandleFailure(cachedPlayer);
    }

    public PlayerController(ISender sender, ILogger<PlayerController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}