namespace AccountService.Exceptions;

public class DuplicationException : Exception
{
    public DuplicationException(string message) : base(message)
    {
    }
}
