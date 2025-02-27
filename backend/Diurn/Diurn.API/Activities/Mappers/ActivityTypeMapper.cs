using Diurn.Contracts;
using Diurn.Core;

namespace Diurn.Activities.Mappers;

public static class ActivityTypeMapper
{
    public static ActivityType ToModel(this ActivityTypeResponse activityType)
        => new()
        {
            Id = activityType.Id,
            Name = activityType.Name,
            Category = activityType.Category.ToModel(),
        };
}

public static class ActivityCategoryMapper
{
    public static ActivityCategory ToModel(this ActivityCategoryResponse activityCategory)
        => (ActivityCategory)activityCategory;
}