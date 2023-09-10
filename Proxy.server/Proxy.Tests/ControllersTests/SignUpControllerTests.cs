using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Proxy.API.Controllers;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Services.Registration;

namespace Proxy.Tests.ControllersTests;

public class SignUpControllerTests
{
    private readonly IRegisterService _registerService;
    public SignUpControllerTests()
    {
        _registerService = A.Fake<IRegisterService>();
    }
    [Fact]
    public async void SignUp_Should_Succeed_With_Valid_Credentials()
    {
    // Arrange
    var loginCredentials = A.Fake<Credentials>();
    var member = A.Fake<Member>();

    A.CallTo(  () => _registerService.RegisterMember(loginCredentials))
        .Returns(Task.FromResult(member));
    
    var sut = new SignUpController(_registerService);
    
    // Act
    var result = await sut.SignUp(loginCredentials);

    // Assert
    result.Should().NotBeNull().And.BeOfType<CreatedResult>();
    }
    
    [Fact]
    public async void SignUp_Should_Fail_With_Invalid_ModelState()
    {
        // Arrange
        var loginCredentials = A.Fake<Credentials>();
    
        var sut = new SignUpController(_registerService);

        sut.ModelState.AddModelError("email", "e-mail inválido");
    
        // Act
        var result = await sut.SignUp(loginCredentials) as BadRequestObjectResult;
        
        // Assert
        var responseMsg = result!.Value as ModelValidationErrors;
        responseMsg!.Errors.Should().Contain("e-mail inválido");
        
        result.Should().NotBeNull().And.BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async void SignUp_Should_Fail_If_Member_Already_Exists()
    {
        // Arrange
        var loginCredentials = A.Fake<Credentials>();
    
        A.CallTo(  () => _registerService.RegisterMember(loginCredentials))
            .Throws(new MemberAlreadyExistsException("Membro já registrado"));
    
        var sut = new SignUpController(_registerService);
    
        // Act
        var result = await sut.SignUp(loginCredentials) as ConflictObjectResult;
        var responseMsg = result!.Value as ModelValidationErrors;
        
        // Assert
        result.Should().NotBeNull().And.BeOfType<ConflictObjectResult>();
        responseMsg!.Errors.Should().Contain("Membro já registrado");
    
    }
}