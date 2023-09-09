using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Proxy.API.Controllers;
using Proxy.API.Models;

namespace Proxy.Tests;

public class LoginControllerTests
{
    [Theory]
    [InlineData("valido@email.com", "123456")]
    public void Login_Should_Succeed_With_Valid_Credentials(string email, string password)
    {
    // Arrange
    LoginCredentials loginCredentials = new(email, password);
    
    var controller = new LoginController();
    
    // Act
    var result = controller.Login(loginCredentials);

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
    }
    
    [Theory]
    [InlineData("emailInválido.com", "123456")]
    public void Login_Should_Fail_With_Invalid_Email(string email, string password)
    {
        // Arrange
        LoginCredentials loginCredentials = new(email, password);
    
        var controller = new LoginController();
        controller.ModelState.AddModelError("email", "e-mail inválido");
    
        // Act
        var result = controller.Login(loginCredentials) as BadRequestObjectResult;
        var responseMsg = result!.Value as ModelValidationErrors;
        
        // Assert
        result.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();

        responseMsg!.Errors.Should().Contain("e-mail inválido");

    }
    
    [Theory]
    [InlineData("válido@email.com", "123")]
    public void Login_Should_Fail_With_Invalid_Password(string email, string password)
    {
        // Arrange
        LoginCredentials loginCredentials = new(email, password);
    
        var controller = new LoginController();
        controller.ModelState.AddModelError("password", "password inválido");
    
        // Act
        var result = controller.Login(loginCredentials) as BadRequestObjectResult;
        var responseMsg = result!.Value as ModelValidationErrors;
        
        // Assert
        result.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();

        responseMsg!.Errors.Should().Contain("password inválido");
    }
}