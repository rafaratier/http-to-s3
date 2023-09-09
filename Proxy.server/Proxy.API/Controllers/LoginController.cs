using Microsoft.AspNetCore.Mvc;
using Proxy.API.Models;

namespace Proxy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(LoginCredentials request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
                .ToList();

            ModelValidationErrors validationResponse = new(errors);
            return BadRequest(validationResponse);
        }
        
        return Ok("token");
    }
}
