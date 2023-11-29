namespace Ynov.Business.Exceptions;

public class InvalidSortQueryException : Exception
{
    public InvalidSortQueryException()
        : base("Sort value do not exists.")
    {
    }

    public InvalidSortQueryException(string message)
        : base(message)
    {
    }
}