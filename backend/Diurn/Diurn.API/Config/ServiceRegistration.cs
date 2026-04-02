using Diurn.Application;
using Diurn.DB;
using TimeProvider = Diurn.Application.TimeProvider;

namespace Diurn.Config;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUser, User>();
        services.AddTransient<ITimeProvider, TimeProvider>();
        
        services.AddTransient<IActivityRepository, ActivityRepository>();
        services.AddTransient<IActivityTypeRepository, ActivityTypeRepository>();
        services.AddTransient<IActivityCategoryRepository, ActivityCategoryRepository>();
        
        services.AddTransient<ActivityService>();
        services.AddTransient<ActivityTypeService>();
        return services;
    }
}