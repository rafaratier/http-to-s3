namespace Proxy.API.Common.PasswordManager;

public class PasswordManager : IPasswordManager
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 4);
    }

    public bool Verify(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, hashedPassword);
    }
}