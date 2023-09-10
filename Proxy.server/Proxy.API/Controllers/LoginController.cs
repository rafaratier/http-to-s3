using Microsoft.AspNetCore.Mvc;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Services;

namespace Proxy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    
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

        try
        {
            _authenticationService.AuthenticateMember(request.Email!, request.Password!);
        }
        catch (InvalidCredentialsException e)
        {
            ModelValidationErrors validationResponse = new(new List<string>(){e.Message});
            return Unauthorized(validationResponse);
        }
        
        return Ok("token");
    }
}
