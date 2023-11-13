using FakeItEasy;
using FluentAssertions;
using Proxy.API.Common.PasswordManager;
using Proxy.API.Exceptions;
using Proxy.API.Models;
using Proxy.API.Persistence;
using Proxy.API.Services.Registration;

namespace Proxy.Tests.ServicesTests;

public class RegisterServiceTests
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordManager _passwordManager;

    public RegisterServiceTests()
    {
        _memberRepository = A.Fake<IMemberRepository>();
        _passwordManager = A.Fake<IPasswordManager>();
    }

    [Fact]
    public async void Registration_Should_Succeed_If_Member_Does_Not_Exist()
    {
        //Arrange
        var credentials = A.Fake<Credentials>();

        A.CallTo(() => _memberRepository.GetMemberByEmailAsync(credentials.Email))
            .Returns(Task.FromResult<Member?>(null));
        
        //Act
        var sut = new RegisterService(_memberRepository, _passwordManager);

        var result = await sut.RegisterMember(credentials);
        
        //Assert
        A.CallTo(() => _passwordManager.Hash(credentials.Password)).MustHaveHappened();

        result.Should().BeOfType<Member>();
    }
    
    [Fact]
    public async void Registration_Should_Fail_If_Member_Already_Exists()
    {
        //Arrange
        var credentials = A.Fake<Credentials>();

        var member = A.Fake<Member>();

        A.CallTo(() => _memberRepository.GetMemberByEmailAsync(credentials.Email))
            .Returns(Task.FromResult<Member?>(member));
        
        //Act
        var sut = new RegisterService(_memberRepository, _passwordManager);
        
        Func<Task> result = () => sut.RegisterMember(credentials);
        
        //Assert
        A.CallTo(() => _passwordManager.Hash(credentials.Password)).MustNotHaveHappened();
        
        await result.Should().ThrowAsync<MemberAlreadyExistsException>().WithMessage("Membro já cadastrado com este email.");
    }
}