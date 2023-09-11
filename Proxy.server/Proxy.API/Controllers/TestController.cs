using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proxy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("PRE-SIGNED-KEY");
    }
}