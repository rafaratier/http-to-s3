using Microsoft.AspNetCore.Mvc;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Services.Registration;

namespace Proxy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SignUpController : ControllerBase
{
    private readonly IRegisterService _registerService;
    public SignUpController(IRegisterService registerService)
    {
        _registerService = registerService;
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUp(Credentials request)
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
            await _registerService.RegisterMember(request);
        }
        catch (MemberAlreadyExistsException e)
        {
            ModelValidationErrors validationResponse = new(new List<string>(){e.Message});
            return Conflict(validationResponse);
        }
        
        return Created(nameof(LoginController), new Member(request.Email));
    }
}