namespace Diurn.Application;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}