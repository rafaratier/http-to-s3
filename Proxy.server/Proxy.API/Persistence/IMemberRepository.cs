using Proxy.API.Models;

namespace Proxy.API.Persistence;

public interface IMemberRepository
{
    public Task<Member> GetByEmailAsync(string email);
}