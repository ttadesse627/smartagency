using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class UserGroupEntityConfig : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(ug => ug.Id);
        builder.HasMany(ug => ug.Permissions)
            .WithOne()
            .HasForeignKey(p => p.GroupId);

        builder.HasMany(ug => ug.AppUsers)
            .WithMany(user => user.UserGroups)
            .UsingEntity<UserGroupUser>(
                j =>
                {
                    j.HasKey(t => new { t.UserGroupId, t.AppUserId });

                    j.HasOne(ugu => ugu.UserGroup)
                        .WithMany()
                        .HasForeignKey(ugu => ugu.UserGroupId);

                    j.HasOne(ugu => ugu.AppUser)
                        .WithMany()
                        .HasForeignKey(ugu => ugu.AppUserId);
                });

    }


}