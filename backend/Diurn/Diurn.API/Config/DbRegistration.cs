using Diurn.DB;
using Diurn.DB.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Diurn.Config;

public static class DbRegistration
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableInterceptor>();
        services.AddDbContext<ApplicationDbContext>();
        return services;
    }
}