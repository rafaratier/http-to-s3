namespace Proxy.API.Common;

public static class PasswordManager
{
    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 2);
    }

    public static bool Verify(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, hashedPassword);
    }
}