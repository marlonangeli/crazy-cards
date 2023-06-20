using CrazyCards.Application.Contracts.Classes;
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

public class ClassController : ApiControllerBase
{
    [HttpPost]
    [Route("", Name = "CreateClassAsync")]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpGet]
    [Route("{id:guid}", Name = "GetClassByIdAsync")]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetClassById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"class:{id}";
        var cachedClass = await _cache.GetOrCallFunctionAsync(
            cacheKey,
            () => Result.Success(new GetClassByIdQuery(id))
                .Bind(query => Sender.Send(query, cancellationToken)),
            TimeSpan.FromMinutes(1),
            cancellationToken);

        return cachedClass!.IsSuccess ? Ok(cachedClass.Value) : HandleFailure(cachedClass);
    }


    public ClassController(ISender sender, ILogger<ClassController> logger, IDistributedCache cache) : base(sender,
        logger)
    {
        _cache = cache;
    }

    private readonly IDistributedCache _cache;
}