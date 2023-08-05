namespace BookService.Services;

public class TimeFactory : ITimeFactory
{
    public long UnixTimeNow()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
