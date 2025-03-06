using Diurn.Application;
using Diurn.Core;

namespace Diurn.DB;

public class ActivityRepository : IActivityRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ActivityRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Activity>> GetAsync(int pageNo, int pageSize)
    {
        var skip = (pageNo - 1) * pageSize;
        return await _dbContext.Activities.Skip(skip).Take(pageSize).ToListAsync();
    }

    public async Task<Activity?> GetAsync(Guid id)
    {
        return await _dbContext.Activities.FindAsync(id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        var newActivity = _dbContext.Activities.Add(activity);
        await _dbContext.SaveChangesAsync();
        return newActivity.Entity;
    }

    public async Task<Activity?> UpdateAsync(Activity activity)
    {
        _dbContext.Entry(activity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return activity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var activity = await _dbContext.Activities.FindAsync(id);
        if (activity is null)
            return false;
        _dbContext.Activities.Remove(activity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}