using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class DeviceAttributeConfiguration : IEntityTypeConfiguration<DeviceAttribute>
{
    public void Configure(EntityTypeBuilder<DeviceAttribute> builder)
    {
        builder.ToTable("device_attributes");
        builder.HasKey(n => n.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.DeviceId).HasColumnName("device_id");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Value).HasColumnName("value");
        builder.Property(x => x.CreatedUtc).HasColumnName("created_utc");
        builder.Property(x => x.UpdatedUtc).HasColumnName("updated_utc");
    }
}