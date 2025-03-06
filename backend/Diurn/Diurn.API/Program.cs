using Diurn.Application;
using Diurn.DB;

namespace Diurn;

public class Program
{
    public static void Main(string[] args)
    {        
        var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

        builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>();
        builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
        builder.Services.AddTransient<IActivityTypeRepository, ActivityTypeRepository>();
        builder.Services.AddTransient<ActivityService>();
        builder.Services.AddTransient<ActivityTypeService>();

        var app = builder.Build();

// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

