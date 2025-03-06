using Diurn.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diurn.DB.Configurations;

public class ActivityCategoryConfiguration : IEntityTypeConfiguration<ActivityCategory>
{
    public void Configure(EntityTypeBuilder<ActivityCategory> builder)
    {
        var data = Enum.GetValues<ActivityCategoryEnum>().Select(value => new ActivityCategory
        {
            Id = (int)value + 1,
            Name = value.ToString(),
        }).ToArray();
        builder.HasData(data);
    }
}