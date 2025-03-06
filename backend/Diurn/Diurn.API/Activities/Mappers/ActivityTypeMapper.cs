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
            CategoryId = activityType.Category.ToModel(),
        };
    
    public static ActivityType ToModel(this ActivityTypeCreate activityType)
        => new()
        {
            Name = activityType.Name,
            CategoryId = activityType.Category.ToModel(),
        };

    public static ActivityTypeResponse ToResponse(this ActivityType activityType)
        => new(
            Id: activityType.Id,
            Name: activityType.Name,
            Category: activityType.CategoryId.ToResponse()
        );

    public static IEnumerable<ActivityTypeResponse> ToResponse(this IEnumerable<ActivityType> activityTypes)
        => activityTypes.Select(ToResponse);
}