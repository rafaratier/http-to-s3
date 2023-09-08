using FluentValidation;

namespace Proxy.API.Members.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.Password).MinimumLength(6);
    }
    
}