using Diurn.Contracts;
using Diurn.Core;

namespace Diurn.Activities.Mappers;

public static class ActivityCategoryMapper
{
    public static ActivityCategoryEnum ToModel(this ActivityCategoryResponse activityCategory)
        => (ActivityCategoryEnum)activityCategory;
    
    public static IEnumerable<ActivityCategoryResponse> ToResponse(this IEnumerable<ActivityCategoryEnum> activityCategories)
        => activityCategories.Select(ToResponse);
    
    public static ActivityCategoryResponse ToResponse(this ActivityCategoryEnum activityCategoryEnum)
        => (ActivityCategoryResponse)activityCategoryEnum;
}