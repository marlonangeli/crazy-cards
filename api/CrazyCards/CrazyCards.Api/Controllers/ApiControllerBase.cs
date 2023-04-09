using Microsoft.AspNetCore.Mvc;

namespace CrazyCards.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    
}