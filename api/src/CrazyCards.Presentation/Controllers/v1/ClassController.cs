using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Core.Classes.Commands;
using CrazyCards.Application.Core.Classes.Commands.CreateClass;
using CrazyCards.Application.Core.Classes.Queries;
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
/// Classes
/// </summary>
public class ClassController : ApiControllerBase
{
    /// <summary>
    /// Criar uma nova classe
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("", Name = "CreateClassAsync")]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateClass(
        [FromBody] CreateClassRequest request,
        CancellationToken cancellationToken)
    {
        var classResponse = await Result.Create(request, Errors.UnProcessableRequest)
            .Map(value => new CreateClassCommand(
                request.Name,
                request.Description,
                request.ImageId,
                request.SkinId))
            .Bind(command => Sender.Send(command, cancellationToken));

        return classResponse.IsSuccess
            ? CreatedAtAction(nameof(GetClassById), new { id = classResponse.Value.Id }, classResponse.Value)
            : HandleFailure(classResponse);
    }

    /// <summary>
    /// Obter classes de forma paginada
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("", Name = "GetClassesAsync")]
    [ProducesResponseType(typeof(PagedList<ClassResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllClasses(
        [FromQuery] GetClassesRequest request,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"classes:{request.Page}:{request.PageSize}:{request.Name}";
        
        var classes = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Sender.Send(
                new GetClassesPaginatedQuery(
                    (int)request.Page,
                    (int)request.PageSize,
                    request.Name), cancellationToken),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        if (classes is null)
        {
            return NoContent();
        }
        
        return Ok(classes);
    }
    
    /// <summary>
    /// Obter uma classe pelo seu identificador
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}", Name = "GetClassByIdAsync")]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetClassById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"class:{id}";
        
        var @class = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Result.Success(new GetClassByIdQuery(id))
                .Bind(query => Sender.Send(query, cancellationToken)),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return @class!.IsSuccess ? Ok(@class.Value) : HandleFailure(@class);
    }


    /// <inheritdoc />
    public ClassController(ISender sender, ILogger<ClassController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}