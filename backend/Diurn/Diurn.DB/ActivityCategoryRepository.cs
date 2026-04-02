using Diurn.Application;
using Diurn.Core;

namespace Diurn.DB;

public class ActivityCategoryRepository : IActivityCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public ActivityCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActivityCategoryEnum>> GetAsync()
    {
        return await _context.ActivityCategories.Select(category => (ActivityCategoryEnum) category.Id).ToListAsync();
    }

    public async Task<ActivityCategoryEnum> GetAsync(int id)
    {
        return await _context.ActivityCategories.Select(category => (ActivityCategoryEnum) category.Id).FirstOrDefaultAsync(category => category == (ActivityCategoryEnum) id);
    }
}