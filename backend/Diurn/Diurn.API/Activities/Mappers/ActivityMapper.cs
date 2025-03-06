using Diurn.Contracts;
using Diurn.Core;

namespace Diurn.Activities.Mappers;

public static class ActivityMapper
{
    public static ActivityResponse ToResponse(this Activity activity)
    {
        return new ActivityResponse(
            Id: activity.Id,
            Name: activity.Name,
            Type: activity.Type.ToResponse()
        );
    }

    public static List<ActivityResponse> ToResponse(this List<Activity> activities)
    {
        return activities.Select(ToResponse).ToList();
    }

    public static Activity ToModel(this ActivityCreate activity)
        => new()
        {
            Type = activity.Type.ToModel(),
            Name = activity.Name,
        };
    
    public static Activity ToModel(this ActivityUpdate activity)
        => new()
        {
            Id = activity.Id,
            Type = activity.Type.ToModel(),
            Name = activity.Name,
        };
}