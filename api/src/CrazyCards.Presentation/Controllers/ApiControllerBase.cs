using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CrazyCards.Presentation.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiControllerBase(ISender sender) => Sender = sender;
}