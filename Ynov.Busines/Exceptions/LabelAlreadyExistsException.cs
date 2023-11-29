namespace Ynov.Business.Exceptions;

public class LabelAlreadyExistsException : Exception
{
    public LabelAlreadyExistsException()
        : base("Label already exists.")
    {
    }

    public LabelAlreadyExistsException(string message)
        : base(message)
    {
    }
}