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

    public async Task<Member> AuthenticateMember(string email, string inputPassword)
    {
        var loginCredentials = await _memberRepository.GetLoginCredentialsByEmailAsync(email);
        // Console.WriteLine(loginCredentials);
        
        if (loginCredentials is null)
        {
            throw new InvalidCredentialsException("Email inválido.");
        }

        if (!(PasswordManager.Verify(inputPassword, loginCredentials.Password!)))
        {
            throw new InvalidCredentialsException("Senha inválida.");
        }

        return new Member(loginCredentials.Email!);
    }
}
// "$2a$13$BR5RPaQZhgQWSqSNk5ogjudfuykrdmKLAxIpAhYZ44aNKE96jftnK"