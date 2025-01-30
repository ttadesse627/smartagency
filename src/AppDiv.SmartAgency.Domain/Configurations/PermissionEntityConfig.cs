using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class PermissionEntityConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(ug => ug.Id);
        builder.HasOne(p => p.Group)
            .WithMany()
            .HasForeignKey(p => p.GroupId);

        builder.HasOne(p => p.Resource)
            .WithMany()
            .HasForeignKey(p => p.ResourceId);

    }


}