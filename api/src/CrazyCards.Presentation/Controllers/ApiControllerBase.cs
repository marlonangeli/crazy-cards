using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using CrazyCards.Domain.Primitives.ValidationResult;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Presentation.Controllers;

/// <summary>
/// ApiControllerBase
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly ILogger<ApiControllerBase> Logger;

    /// <inheritdoc />
    protected ApiControllerBase(ISender sender, ILogger<ApiControllerBase> logger)
    {
        Sender = sender;
        Logger = logger;
    }

    /// <summary>
    /// HandleFailure
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error, validationResult.Errors)),
            null => 
                NotFound(
                    CreateProblemDetails(
                        "Not Found", StatusCodes.Status404NotFound,
                        new Error("NotFound", "O recurso solicitado não foi encontrado."))),
            _ => BadRequest(
                CreateProblemDetails(
                    "Bad Request", StatusCodes.Status400BadRequest,
                    result.Error))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) => new()
    {
        Title = title,
        Status = status,
        Detail = error.Message,
        Type = error.Code,
        Extensions =
            { { nameof(errors), errors } }
    };
}