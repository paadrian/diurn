using Diurn.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diurn.DB.Configurations;

public class ActivityCategoryConfiguration : IEntityTypeConfiguration<ActivityCategory>
{
    public void Configure(EntityTypeBuilder<ActivityCategory> builder)
    {
        builder.HasMany(category => category.ActivityTypes)
            .WithOne(activityType => activityType.Category)
            .HasForeignKey(activityType => activityType.CategoryId);
        
        var data = Enum.GetValues<ActivityCategoryEnum>().Select(value => new ActivityCategory
        {
            Id = value,
            Name = value.ToString(),
        }).ToArray();
        builder.HasData(data);
    }
}