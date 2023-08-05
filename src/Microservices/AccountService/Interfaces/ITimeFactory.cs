namespace AccountService.Interfaces;

public interface ITimeFactory
{
    /// <summary>
    /// Returns the amount of seconds elapsed since 1.1.1970
    /// </summary>
    long UnixTimeNow();

    /// <summary>
    /// Converts a given date into the amount of seconds elapsed since 1.1.1970
    /// </summary>
    long DateOnlyToUnixTime(DateOnly dateOnly);

    /// <summary>
    /// Converts a unix timestamp into its corresponding Date representation
    /// </summary>
    string UnixTimeToDateString(long unixTimestamp);
}
