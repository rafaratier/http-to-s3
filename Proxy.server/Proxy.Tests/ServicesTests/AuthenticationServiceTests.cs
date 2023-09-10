using FakeItEasy;
using FluentAssertions;
using Proxy.API.Common;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Persistence;
using Proxy.API.Services;
using Proxy.API.Services.Authentication;

namespace Proxy.Tests.ServicesTests;

public class AuthenticationServiceTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordManager _passwordManager;

    public AuthenticationServiceTests()
    {
        _passwordManager = A.Fake<IPasswordManager>();
        _memberRepository = A.Fake<IMemberRepository>();
    }
    
    [Fact]
    public async void Authentication_Should_Succeed_With_Valid_Credentials()
    {
        //Arrange
        var loginCredentials = A.Fake<Credentials>();

        A.CallTo(() => _memberRepository.GetLoginCredentialsByEmailAsync(loginCredentials.Email))
            .Returns(Task.FromResult<Credentials?>(loginCredentials));
        
        A.CallTo(() => _passwordManager.Verify(loginCredentials.Password, loginCredentials.Password)).Returns(true);
        
        var sut = new AuthenticationService(_memberRepository, _passwordManager);
        
        //Act
        var result = await sut.AuthenticateMember(loginCredentials);

        //Assert
        result.Should().BeOfType<Member>();
    }

    [Fact]
    public async void Authentication_Should_Fail_If_Member_Not_Registered()
    {
        //Arrange
        var loginCredentials = A.Fake<Credentials>();

        A.CallTo(() => _memberRepository.GetLoginCredentialsByEmailAsync(loginCredentials.Email))
            .Returns(Task.FromResult<Credentials?>(null));
        
        var sut = new AuthenticationService(_memberRepository, _passwordManager);
        
        //Act
        Func<Task> act = () => sut.AuthenticateMember(loginCredentials);

        //Assert
        await act.Should().ThrowAsync<InvalidCredentialsException>().WithMessage("Email inválido.");
    }
    
    [Theory]
    [InlineData("123")]
    public async void Authentication_Should_Fail_With_Invalid_Password(string inputPassword)
    {
        //Arrange
        var loginCredentials = A.Fake<Credentials>();
        
        A.CallTo(() => _passwordManager.Verify(inputPassword, loginCredentials.Password)).Returns(false);
        
        var sut = new AuthenticationService(_memberRepository, _passwordManager);
        
        //Act
        Func<Task> act = () => sut.AuthenticateMember(loginCredentials);

        //Assert
        await act.Should().ThrowAsync<InvalidCredentialsException>().WithMessage("Senha inválida.");
    }
}