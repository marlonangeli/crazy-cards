using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Core.Heroes.Commands;
using CrazyCards.Application.Core.Heroes.Queries;
using CrazyCards.Domain.Primitives.Result;
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
            .Map(value => new CreateHeroCommand(
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
    
    [HttpGet]
    [Route("id:guid", Name = "GetHeroByIdAsync")]
    [ProducesResponseType(typeof(HeroResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHeroById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var heroResponse = await Result.Create(id, Errors.UnProcessableRequest)
            .Map(value => new GetHeroByIdQuery(value))
            .Bind(query => Sender.Send(query, cancellationToken));
        
        return heroResponse.IsSuccess 
            ? Ok(heroResponse.Value) 
            : HandleFailure(heroResponse);
    }

    /// <inheritdoc />
    public HeroController(ISender sender, ILogger<HeroController> logger, IDistributedCache cache) : base(sender, logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}