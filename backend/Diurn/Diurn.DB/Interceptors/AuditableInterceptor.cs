using Diurn.Application;
using Diurn.Core.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Diurn.DB.Interceptors;

public class AuditableInterceptor : SaveChangesInterceptor
{
    private readonly IUser _user;
    private readonly ITimeProvider _timeProvider;

    public AuditableInterceptor(IUser user, ITimeProvider timeProvider)
    {
        _user = user;
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<IAuditEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _user.Username;
                entry.Entity.CreatedOn = _timeProvider.UtcNow;
            }
            
            entry.Entity.ModifiedBy = _user.Username;
            entry.Entity.ModifiedOn = _timeProvider.UtcNow;
        }
    }
}