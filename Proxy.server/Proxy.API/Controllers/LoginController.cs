using Microsoft.AspNetCore.Mvc;
using Proxy.API.Common.TokenManager;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Services;

namespace Proxy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenProvider _jwtProvider;
    public LoginController(IAuthenticationService authenticationService, ITokenProvider jwtProvider)
    {
        _authenticationService = authenticationService;
        _jwtProvider = jwtProvider;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(Credentials request)
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
            await _authenticationService.AuthenticateMember(request);
        }
        catch (InvalidCredentialsException e)
        {
            ModelValidationErrors validationResponse = new(new List<string>(){e.Message});
            return Unauthorized(validationResponse);
        }

        var token = _jwtProvider.Generate(request.Email);
        return Ok(token);
    }
}
