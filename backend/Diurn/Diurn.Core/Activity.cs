using Diurn.Core.Common;

namespace Diurn.Core;

public class Activity : AuditEntity<Guid>
{
    public required string Name { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public Guid TypeId { get; init; } 
    public ActivityType Type { get; init; }
}

public class ActivityType : Entity<Guid>
{
    public required string Name { get; init; }
    public ActivityCategoryEnum CategoryId { get; init; }
    public ActivityCategory Category { get; init; } = null!;
}

public class ActivityCategory : Entity<ActivityCategoryEnum>
{
    public string Name { get; set; } = string.Empty;
    public ICollection<ActivityType> ActivityTypes { get; set; } = [];
}

public enum ActivityCategoryEnum
{
    None = 1,
    Physical,
    Mental,
    Both
}