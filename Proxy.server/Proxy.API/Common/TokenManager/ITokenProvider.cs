namespace Proxy.API.Common.TokenManager;

public interface ITokenProvider
{
    string Generate(string email);
}