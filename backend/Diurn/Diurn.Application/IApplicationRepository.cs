using Diurn.Core;

namespace Diurn.Application;

public interface IApplicationRepository
{
    Task<List<Activity>> GetAsync();
    Task<Activity?> GetAsync(Guid id);
    
    Task<Activity> CreateAsync(Activity activity);
    Task<Activity> UpdateAsync(Activity activity);
    Task<bool> DeleteAsync(Guid id);
}