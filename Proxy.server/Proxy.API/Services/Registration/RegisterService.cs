using Proxy.API.Common;
using Proxy.API.Common.PasswordManager;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Persistence;

namespace Proxy.API.Services.Registration;

public class RegisterService : IRegisterService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordManager _passwordManager;
    public RegisterService(IMemberRepository memberRepository, IPasswordManager passwordManager)
    {
        _memberRepository = memberRepository;
        _passwordManager = passwordManager;
    }
    public async Task<Member> RegisterMember(Credentials credentials)
    {
        Member? member = await _memberRepository.GetMemberByEmailAsync(credentials.Email);

        if (member is not null)
        {
            throw new MemberAlreadyExistsException("Membro já cadastrado com este email.");
        }

        var hashedPassword = _passwordManager.Hash(credentials.Password);

        await _memberRepository.Save(credentials.Email, hashedPassword);

        return new Member(credentials.Email);
    }
}