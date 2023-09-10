namespace Proxy.API.Common;

public interface IPasswordManager
{
    public string Hash(string password);

    public bool Verify(string inputPassword, string hashedPassword);
}