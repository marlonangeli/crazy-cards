using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Core.Heroes.Commands;
using CrazyCards.Application.Core.Heroes.Queries;
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
/// Heróis
/// </summary>
public class HeroController : ApiControllerBase
{
    /// <summary>
    /// Criar um novo herói
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("", Name = "CreateHeroAsync")]
    [ProducesResponseType(typeof(HeroResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateHero(
        [FromBody] CreateHeroRequest request,
        CancellationToken cancellationToken)
    {
        var heroResponse = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(_ => new CreateHeroCommand(
                request.Name,
                request.Description,
                request.ImageId,
                request.SkinId,
                request.ClassId,
                request.WeaponId))
            .Bind(command => Sender.Send(command, cancellationToken));
        
        return heroResponse.IsSuccess 
            ? CreatedAtAction(nameof(GetHeroById), new { id = heroResponse.Value.Id }, heroResponse.Value) 
            : HandleFailure(heroResponse);
    }
    
    /// <summary>
    /// Obter heróis de forma paginada
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("", Name = "GetHeroesAsync")]
    [ProducesResponseType(typeof(PagedList<HeroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHeroes(
        [FromQuery] GetHeroesRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"heroes:{request.Page}:{request.PageSize}:{request.Name}";

        var heroes = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(
                new GetHeroesPaginatedQuery(
                    (int)request.Page, 
                    (int)request.PageSize,
                    request.Name), cancellationToken),
            TimeSpan.FromMinutes(5),
            cancellationToken);
        
        return heroes is not null && heroes.Items.Any() 
            ? Ok(heroes) 
            : NoContent();
    }
    
    /// <summary>
    /// Obter um herói pelo seu identificador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}", Name = "GetHeroByIdAsync")]
    [ProducesResponseType(typeof(HeroResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHeroById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"hero:{id}";
        
        var hero = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetHeroByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(5),
            cancellationToken);
        
        return hero!.IsSuccess 
            ? Ok(hero.Value) 
            : HandleFailure(hero);
    }

    /// <inheritdoc />
    public HeroController(ISender sender, ILogger<HeroController> logger, IDistributedCache cache) : base(sender, logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}