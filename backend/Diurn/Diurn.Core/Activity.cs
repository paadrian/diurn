using Diurn.Core.Common;

namespace Diurn.Core;

public class Activity : AuditEntity<Guid>
{
    public required ActivityType Type { get; init; }
    public required string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class ActivityType : Entity<Guid>
{
    public required string Name { get; init; }
    public ActivityCategoryEnum CategoryId { get; init; }
    public ActivityCategory Category { get; init; } = null!;
}

public class ActivityCategory : Entity<int>
{
    public string Name { get; set; } = string.Empty;
}

public enum ActivityCategoryEnum
{
    None = 0,
    Physical,
    Mental,
    Both
}