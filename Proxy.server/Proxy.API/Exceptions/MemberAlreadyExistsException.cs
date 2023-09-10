namespace Proxy.API.Exceptions;

public class MemberAlreadyExistsException : Exception
{
    public MemberAlreadyExistsException()
    {
        
    }
    public MemberAlreadyExistsException(string message) : base(message)
    {
        
    }
    
}