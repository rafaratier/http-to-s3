using System.Security.Authentication;
using Proxy.API.Common;
using Proxy.API.Exceptions;
using Proxy.API.Models;

namespace Proxy.API.Services;

public class AuthenticationService : IAuthenticationService
{
    // private readonly IMemberRepository _memberRepository;
    // public AuthenticationService(IMemberRepository memberRepository)
    // {
    //     _memberRepository = memberRepository;
    // }

    public Member AuthenticateMember(string email, string inputPassword)
    {
        // var loginCredentials = await _memberRepository.GetLoginCredentialsByEmailAsync(email);
        LoginCredentials loginCredentials = new(email, inputPassword);
        
        if (loginCredentials is null)
        {
            throw new InvalidCredentialsException("Email inválido.");
        }

        if (!(PasswordManager.Verify(inputPassword, "$2a$13$BR5RPaQZhgQWSqSNk5ogjudfuykrdmKLAxIpAhYZ44aNKE96jftnK")))
        {
            throw new InvalidCredentialsException("Senha inválida.");
        }

        return new Member(loginCredentials.Email!);
    }
}