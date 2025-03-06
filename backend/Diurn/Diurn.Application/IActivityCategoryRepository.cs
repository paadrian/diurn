using Diurn.Core;

namespace Diurn.Activities.Controllers;

public interface IActivityCategoryRepository
{
    Task<IEnumerable<ActivityCategoryEnum>> GetAsync();
    Task<ActivityCategoryEnum> GetAsync(int id);
}