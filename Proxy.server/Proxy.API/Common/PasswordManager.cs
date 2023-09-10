namespace Proxy.API.Common;

public class PasswordManager : IPasswordManager
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 2);
    }

    public bool Verify(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, hashedPassword);
    }
}