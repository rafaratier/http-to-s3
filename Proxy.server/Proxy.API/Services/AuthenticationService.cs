using Proxy.API.Common;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Persistence;

namespace Proxy.API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMemberRepository _memberRepository;
    public AuthenticationService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Member> AuthenticateMember(LoginCredentials credentials)
    {
        var loginCredentials = await _memberRepository.GetLoginCredentialsByEmailAsync(credentials.Email);
        
        if (loginCredentials is null)
        {
            throw new InvalidCredentialsException("Email inválido.");
        }

        if (!(PasswordManager.Verify(credentials.Password, loginCredentials.Password!)))
        {
            throw new InvalidCredentialsException("Senha inválida.");
        }

        return new Member(loginCredentials.Email!);
    }
}
// "$2a$13$BR5RPaQZhgQWSqSNk5ogjudfuykrdmKLAxIpAhYZ44aNKE96jftnK"