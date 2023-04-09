using Microsoft.AspNetCore.Mvc;

namespace CrazyCards.Api.Controllers.v1;

public class FooController : ApiControllerBase
{
    public IActionResult Get()
    {
        return Ok("Hello World!");
    }
}