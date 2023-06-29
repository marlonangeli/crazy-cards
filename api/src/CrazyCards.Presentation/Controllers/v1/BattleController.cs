using CrazyCards.Application.Contracts.Game;
using CrazyCards.Application.Core.Game.Commands;
using CrazyCards.Application.Core.Game.Queries;
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
/// Batalhas
/// </summary>
public class BattleController : ApiControllerBase
{
    /// <summary>
    /// Criar nova sala de espera
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("wait", Name = "WaitForBattleAsync")]
    [ProducesResponseType(typeof(WaitingRoomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> WaitForBattle(
        [FromBody] WaitForBattleRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(_ => new WaitForBattleCommand(request.PlayerId, request.BattleDeckId))
            .Bind(command => Sender.Send(command, cancellationToken));
        
        return response.IsSuccess
            ? CreatedAtAction(nameof(GetWaitingRoom), new { id = response.Value.Id }, response.Value)
            : HandleFailure(response);
    }
    
    /// <summary>
    /// Obter sala de espera
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("waitingroom/{id:guid}", Name = "GetWaitingRoomAsync")]
    [ProducesResponseType(typeof(WaitingRoomResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetWaitingRoom(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"waitingroom:{id}";
        
        var waitingRoom = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetWaitingRoomByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(1),
            cancellationToken);
        
        return waitingRoom is { IsSuccess: true }
            ? Ok(waitingRoom.Value)
            : HandleFailure(waitingRoom);
    }
    
    /// <summary>
    /// Iniciar batalha
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("battle", Name = "StartBattleAsync")]
    [ProducesResponseType(typeof(BattleResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> StartBattle(
        [FromBody] Guid id,
        CancellationToken cancellationToken)
    {
        var response = await Result.Create(id, Errors.UnProcessableRequest)
            .Map(_ => new StartBattleCommand(id))
            .Bind(command => Sender.Send(command, cancellationToken));
        
        return response.IsSuccess
            ? CreatedAtAction(nameof(GetBattle), new { id = response.Value.Id }, response.Value)
            : HandleFailure(response);
    }

    /// <summary>
    /// Obter batalha
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}", Name = "GetBattleAsync")]
    [ProducesResponseType(typeof(BattleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBattle(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"battle:{id}";
        
        var battle = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetBattleByIdQuery(id), cancellationToken),
TimeSpan.FromMinutes(1),
            cancellationToken);
        
        return battle is { IsSuccess: true }
            ? Ok(battle.Value)
            : HandleFailure(battle);
    }


    /// <inheritdoc />
    public BattleController(ISender sender, ILogger<BattleController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}