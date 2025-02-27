using System.Reflection;
using Diurn.Core;
using Microsoft.Extensions.Configuration;

namespace Diurn.DB;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Activity> Activities { get; set; }
    
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}