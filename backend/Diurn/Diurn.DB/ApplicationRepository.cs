using Diurn.Application;
using Diurn.Core;

namespace Diurn.DB;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ApplicationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Activity>> GetAsync()
    {
        return await _dbContext.Activities.ToListAsync();
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

    public async Task<Activity> UpdateAsync(Activity activity)
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