using Diurn.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diurn.DB.Configurations;

public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.Property(e => e.CategoryId).HasConversion<int>();

        builder.HasOne(activityType => activityType.Category)
            .WithMany(category => category.ActivityTypes)
            .HasForeignKey(category => category.CategoryId);
    }
}