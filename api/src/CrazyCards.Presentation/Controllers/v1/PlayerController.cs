using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Players;
using CrazyCards.Application.Core.Player.Commands;
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
    /// <summary>
    /// Criar um novo jogador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login", Name = "LoginAsync")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var loginResponse = await Result.Success(new LoginCommand(
                request.Username,
                request.Password))
            .Bind(command => Sender.Send(command, cancellationToken));

        return loginResponse.IsSuccess
            ? Ok(loginResponse.Value)
            : HandleFailure(loginResponse);
    }

    /// <summary>
    /// Obter jogadores de forma paginada
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("", Name = "GetPlayersAsync")]
    [ProducesResponseType(typeof(PagedList<PlayerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayersPaginated(
        [FromQuery] GetPlayersRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"players:{request.Page}:{request.PageSize}:{request.Username}:{request.Email}";

        var cachedPlayers = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetPlayersPaginatedQuery(
                (int)request.Page,
                (int)request.PageSize,
                request.Username,
                request.Email), cancellationToken),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        if (cachedPlayers is null)
        {
            return NoContent();
        }

        return Ok(cachedPlayers);
    }

    /// <summary>
    /// Obter um jogador pelo seu identificador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <inheritdoc />
    public PlayerController(ISender sender, ILogger<PlayerController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}