using Diurn.Core;

namespace Diurn.Application;

public class ActivityService
{
    private readonly IApplicationRepository _applicationRepository;

    public ActivityService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<List<Activity>> GetAsync()
    {
        return await _applicationRepository.GetAsync();
    }

    public async Task<Activity?> GetAsync(Guid id)
    {
        return await _applicationRepository.GetAsync(id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        return await _applicationRepository.CreateAsync(activity);
    }
    
    public async Task<Activity> UpdateAsync(Activity activity)
    {
        return await _applicationRepository.UpdateAsync(activity);
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _applicationRepository.DeleteAsync(id);
    }
}