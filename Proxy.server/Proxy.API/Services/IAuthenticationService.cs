using Proxy.API.Models;

namespace Proxy.API.Services;

public interface IAuthenticationService
{
    public Member AuthenticateMember(string email, string inputPassword);
}