using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proxy.API.Common.AwsManager;

namespace Proxy.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class S3PresignerController : ControllerBase
{
    private readonly IPresignedUrlProvider _urlProvider;

    public S3PresignerController(IPresignedUrlProvider urlProvider)
    {
        _urlProvider = urlProvider;
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public IActionResult Presigner(string id)
    {
        
        var url = _urlProvider.Generate(id);

        return Ok(new { url });
    }

}