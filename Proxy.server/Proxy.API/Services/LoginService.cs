using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Persistence;

namespace Proxy.API.Services;

public class LoginService
{
    private IMemberRepository _memberRepository;
    public LoginService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;

    }

    public async Task<Member> Login(string email)
    {
        Member? member = await _memberRepository.GetByEmailAsync(email);

        if (member is null)
        {
            throw new MemberNotFoundException();
        }

        return member;
    }
}