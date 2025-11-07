using Diurn.Core;

namespace Diurn.Application;

public interface IActivityCategoryRepository
{
    Task<List<ActivityCategoryEnum>> GetAsync();
    Task<ActivityCategoryEnum> GetAsync(int id);
}