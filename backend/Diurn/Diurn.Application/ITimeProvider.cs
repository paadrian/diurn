namespace Diurn.Application;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}

public class TimeProvider : ITimeProvider
{
    public DateTime UtcNow { get; }
}