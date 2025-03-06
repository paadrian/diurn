using Diurn.Application;
using Diurn.Core;

namespace Diurn.DB;

public class ActivityTypeRepository : IActivityTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ActivityTypeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ActivityType>> GetAsync()
    {
        return await _dbContext.ActivityTypes.ToListAsync();
    }

    public async Task<ActivityType?> GetAsync(Guid id)
    {
        return await _dbContext.ActivityTypes.FindAsync(id);
    }

    public async Task<ActivityType> CreateAsync(ActivityType activity)
    {
        var newActivity = _dbContext.ActivityTypes.Add(activity);
        await _dbContext.SaveChangesAsync();
        return newActivity.Entity;
    }

    public async Task<ActivityType?> UpdateAsync(ActivityType activity)
    {
        _dbContext.Entry(activity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return activity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var activity = await _dbContext.ActivityTypes.FindAsync(id);
        if (activity is null)
            return false;
        _dbContext.ActivityTypes.Remove(activity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}