namespace Ynov.Business.Exceptions;

public class UserPasswordException : Exception
{
    public UserPasswordException()
        : base("Incorrect password")
    {
    }

    public UserPasswordException(string message)
        : base(message)
    {
    }
    
}
