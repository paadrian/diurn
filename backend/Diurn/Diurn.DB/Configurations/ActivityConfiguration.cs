using Diurn.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diurn.DB.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        throw new NotImplementedException();
    }
}