using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Proxy.API.Common.TokenManager;
using Proxy.API.Controllers;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Services;

namespace Proxy.Tests.ControllersTests;

public class LoginControllerTests
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenProvider _tokenProvider;
    public LoginControllerTests()
    {
        _authenticationService = A.Fake<IAuthenticationService>();
        _tokenProvider = A.Fake<ITokenProvider>();
    }
    
    [Fact]
    public async void Login_Should_Succeed_With_Valid_Credentials()
    {
    // Arrange
    var loginCredentials = A.Fake<Credentials>();
    var member = A.Fake<Member>();

    A.CallTo(  () => _authenticationService.AuthenticateMember(loginCredentials))
        .Returns(Task.FromResult(member));
    
    var sut = new LoginController(_authenticationService, _tokenProvider);
    
    // Act
    var result = await sut.Login(loginCredentials);

    // Assert
    result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
    }
    
    [Fact]
    public async void Login_Should_Fail_With_Invalid_ModelState()
    {
        // Arrange
        var loginCredentials = A.Fake<Credentials>();
    
        var sut = new LoginController(_authenticationService, _tokenProvider);

        sut.ModelState.AddModelError("email", "e-mail inválido");
    
        // Act
        var result = await sut.Login(loginCredentials) as BadRequestObjectResult;
        
        // Assert
        var responseMsg = result!.Value as ModelValidationErrors;
        responseMsg!.Errors.Should().Contain("e-mail inválido");
        
        result.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async void Login_Should_Fail_With_Invalid_Credentials()
    {
        // Arrange
        var loginCredentials = A.Fake<Credentials>();

        A.CallTo(  () => _authenticationService.AuthenticateMember(loginCredentials))
            .Throws(new InvalidCredentialsException("Credencias inválias"));
    
        var sut = new LoginController(_authenticationService, _tokenProvider);
    
        // Act
        var result = await sut.Login(loginCredentials) as UnauthorizedObjectResult;
        var responseMsg = result!.Value as ModelValidationErrors;
        
        // Assert
        result.Should().NotBeNull().And.BeOfType<UnauthorizedObjectResult>();
        responseMsg!.Errors.Should().Contain("Credencias inválias");

    }
}