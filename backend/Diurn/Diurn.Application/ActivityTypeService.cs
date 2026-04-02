using Diurn.Core;

namespace Diurn.Application;

public class ActivityTypeService
{
    private readonly IActivityTypeRepository _activityRepository;

    public ActivityTypeService(IActivityTypeRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<List<ActivityType>> GetAsync()
    {
        return await _activityRepository.GetAsync();
    }

    public async Task<ActivityType?> GetAsync(Guid id)
    {
        return await _activityRepository.GetAsync(id);
    }

    public async Task<ActivityType> CreateAsync(ActivityType activity)
    {
        return await _activityRepository.CreateAsync(activity);
    }
    
    public async Task<ActivityType?> UpdateAsync(ActivityType activity)
    {
        return await _activityRepository.UpdateAsync(activity);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _activityRepository.DeleteAsync(id);
    }
}