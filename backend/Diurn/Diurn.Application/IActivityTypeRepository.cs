using Diurn.Core;

namespace Diurn.Application;

public interface IActivityTypeRepository
{
    Task<List<ActivityType>> GetAsync();
    Task<ActivityType?> GetAsync(Guid id);
    
    Task<ActivityType> CreateAsync(ActivityType activity);
    Task<ActivityType?> UpdateAsync(ActivityType activity);
    Task<bool> DeleteAsync(Guid id);
}