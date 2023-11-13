namespace Proxy.API.Common.PasswordManager;

public interface IPasswordManager
{
    public string Hash(string password);

    public bool Verify(string inputPassword, string hashedPassword);
}