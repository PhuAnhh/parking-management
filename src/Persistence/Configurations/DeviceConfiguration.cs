using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");
        builder.HasKey(n => n.Id);
        builder.HasQueryFilter(x => !x.Deleted);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Type).HasColumnName("type").HasConversion<string>();
        builder.Property(x => x.ParentId).HasColumnName("parent_id");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.Deleted).HasColumnName("deleted");
        builder.Property(x => x.CreatedUtc).HasColumnName("created_utc");
        builder.Property(x => x.UpdatedUtc).HasColumnName("updated_utc");
        builder.HasMany(x => x.Attributes).WithOne(x => x.Device).HasForeignKey(x => x.DeviceId);
    }
}