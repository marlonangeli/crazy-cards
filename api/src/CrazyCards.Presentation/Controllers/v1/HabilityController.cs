using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Core.Hability.Commands;
using CrazyCards.Application.Core.Hability.Queries;
using CrazyCards.Domain.Enum;
using CrazyCards.Domain.Enum.Shared;
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
/// Habilidades
/// </summary>
public class HabilityController : ApiControllerBase
{
    [HttpPost]
    [Route("", Name = "CreateHabilityAsync")]
    [ProducesResponseType(typeof(HabilityResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateHability(
        [FromBody] CreateHabilityRequest request,
        CancellationToken cancellationToken)
    {
        var habilityResponse = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(_ => new CreateHabilityCommand(
                request.CardId,
                request.Action,
                HabilityType.FromValue(request.Type)))
            .Bind(command => Sender.Send(command, cancellationToken));

        return habilityResponse.IsSuccess
            ? CreatedAtAction(nameof(GetHabilityById), new { id = habilityResponse.Value.Id }, habilityResponse.Value)
            : HandleFailure(habilityResponse);
    }

    [HttpGet]
    [Route("", Name = "GetHabilitiesAsync")]
    [ProducesResponseType(typeof(PagedList<HabilityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHabilities(
        [FromQuery] GetPaginatedRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"habilities:{request.Page}:{request.PageSize}";

        var habilities = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(
                new GetHabilitiesPaginatedQuery(
                    (int)request.Page,
                    (int)request.PageSize), cancellationToken),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return habilities != null && habilities.Items.Any()
            ? Ok(habilities)
            : NoContent();
    }

    [HttpGet]
    [Route("{id:guid}", Name = "GetHabilityByIdAsync")]
    [ProducesResponseType(typeof(HabilityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHabilityById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"hability:{id}";

        var hability = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(new GetHabilityByIdQuery(id), cancellationToken),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return hability != null
            ? Ok(hability)
            : NotFound();
    }

    [HttpGet]
    [Route("types", Name = "GetHabilityTypesAsync")]
    [ProducesResponseType(typeof(IEnumerator<HabilityType>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetHabilityTypes()
        => Ok(Enumeration<HabilityType>.All);

    /// <inheritdoc />
    public HabilityController(ISender sender, ILogger<HabilityController> logger, IDistributedCache cache) : base(
        sender, logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}