using Diurn.Core;

namespace Diurn.Application;

public interface IActivityRepository
{
    Task<List<Activity>> GetAsync(int pageNo, int pageSize);
    Task<Activity?> GetAsync(Guid id);
    
    Task<Activity> CreateAsync(Activity activity);
    Task<Activity?> UpdateAsync(Activity activity);
    Task<bool> DeleteAsync(Guid id);
}