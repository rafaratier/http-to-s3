using Microsoft.AspNetCore.Mvc;
using Proxy.API.Models;

namespace Proxy.API.Members.Login;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody]LoginCredentials request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        return Ok();
    }
}