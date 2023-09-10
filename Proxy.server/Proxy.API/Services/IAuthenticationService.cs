using Proxy.API.Models;

namespace Proxy.API.Services;

public interface IAuthenticationService
{
    public Task<Member> AuthenticateMember(string email, string inputPassword);
}