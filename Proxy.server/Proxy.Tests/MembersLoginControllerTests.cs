using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Proxy.API.Members.Login;

namespace Proxy.Tests;

public class MembersLoginControllerTests
{
    [Theory]
    [InlineData("valido@email.com", "123456")]
    public void Login_Should_Succeed_With_Valid_Credentials(string email, string password)
    {
    // Arrange

    LoginRequest loginRequest = new(email, password);
    
    var controller = new LoginController();
    
    // Act

    var result = controller.Login(loginRequest);

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
    }
    
    [Theory]
    [InlineData("emailInválido.com", "123456")]
    public void Login_Should_Fail_With_Invalid_Email(string email, string password)
    {
        // Arrange

        LoginRequest loginRequest = new(email, password);
    
        var controller = new LoginController();
    
        // Act

        var result = controller.Login(loginRequest) as UnauthorizedObjectResult;
        var errorMsg = result!.Value;

        // Assert
        result.Should().NotBeNull().And.BeOfType<UnauthorizedObjectResult>();

        errorMsg.Should().BeEquivalentTo(new
        {
            erro = "Email e/ou senha inválidos! Cheque suas credencias e tente novamente."
        });
    }
    
    [Theory]
    [InlineData("válido@email.com", "123")]
    public void Login_Should_Fail_With_Invalid_Password(string email, string password)
    {
        // Arrange

        LoginRequest loginRequest = new(email, password);
    
        var controller = new LoginController();
    
        // Act

        var result = controller.Login(loginRequest) as UnauthorizedObjectResult;
        var errorMsg = result!.Value;

        // Assert
        result.Should().NotBeNull().And.BeOfType<UnauthorizedObjectResult>();

        errorMsg.Should().BeEquivalentTo(new
        {
            erro = "Email e/ou senha inválidos! Cheque suas credencias e tente novamente."
        });
    }
}