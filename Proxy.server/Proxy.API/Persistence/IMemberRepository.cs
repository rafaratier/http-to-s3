using Proxy.API.Models;

namespace Proxy.API.Persistence;

public interface IMemberRepository
{
    public Task<Member> GetMemberByEmailAsync(string email);

    public Task<LoginCredentials> GetLoginCredentialsByEmailAsync(string email);
}