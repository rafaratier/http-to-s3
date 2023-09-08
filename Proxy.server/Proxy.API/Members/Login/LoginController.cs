using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Proxy.API.Members.Login;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public IActionResult Login([FromBody]LoginRequest request)
    {
        LoginRequestValidator validator = new();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Unauthorized(new
            {
                erro = "Email e/ou senha inválidos! Cheque suas credencias e tente novamente."
            });
        }
        
        return Ok();
    }
}