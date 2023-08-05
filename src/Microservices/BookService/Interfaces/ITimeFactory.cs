namespace BookService.Interfaces;

public interface ITimeFactory
{
    /// <summary>
    /// Returns the amount of seconds elapsed since 1.1.1970
    /// </summary>
    long UnixTimeNow();
}
