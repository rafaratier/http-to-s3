using Proxy.API.Models;

namespace Proxy.API.Services.Registration;

public interface IRegisterService
{
    public Task<Member> RegisterMember(Credentials credentials);
}