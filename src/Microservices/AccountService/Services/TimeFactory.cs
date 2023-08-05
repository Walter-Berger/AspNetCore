namespace AccountService.Services;

public class TimeFactory : ITimeFactory
{
    public long UnixTimeNow()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    public long DateOnlyToUnixTime(DateOnly dateOnly)
    {
        var timestampUnix = new DateTimeOffset(dateOnly.Year, dateOnly.Month, dateOnly.Day,
            0, 0, 0, new TimeSpan(0, 0, 0, 0)).ToUnixTimeSeconds();
        return timestampUnix;
    }

    public string UnixTimeToDateString(long unixTimestamp)
    {
        var offset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
        var dateTime = offset.UtcDateTime;
        var dateString = Convert.ToString(dateTime.Date.ToString("yyyy-MM-dd"));
        return dateString;
    }
}
