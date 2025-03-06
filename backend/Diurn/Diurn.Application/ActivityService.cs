using Diurn.Core;

namespace Diurn.Application;

public class ActivityService
{
    private readonly IActivityRepository _activityRepository;

    public ActivityService(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<List<Activity>> GetAsync(int pageNo, int pageSize)
    {
        return await _activityRepository.GetAsync(pageNo, pageSize);
    }

    public async Task<Activity?> GetAsync(Guid id)
    {
        return await _activityRepository.GetAsync(id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        return await _activityRepository.CreateAsync(activity);
    }
    
    public async Task<Activity?> UpdateAsync(Activity activity)
    {
        return await _activityRepository.UpdateAsync(activity);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _activityRepository.DeleteAsync(id);
    }
}