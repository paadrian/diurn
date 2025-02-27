using Diurn.Core.Common;

namespace Diurn.Core;

public class Activity : Entity
{
    public required ActivityType Type { get; init; }
    public required string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class ActivityType : Entity
{
    public required string Name { get; init; }
    public ActivityCategory Category { get; init; }
}

public enum ActivityCategory
{
    None = 0,
    Physical,
    Mental,
    Both
}