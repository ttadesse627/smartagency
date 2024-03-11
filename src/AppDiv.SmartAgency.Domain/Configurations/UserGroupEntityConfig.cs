using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class UserGroupEntityConfig : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasKey(role => role.Id);
        builder.HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(ug => ug.AppUsers)
            .WithMany(user => user.UserGroups);

        builder.HasData(SeedUserGroups());

    }

    internal static List<UserGroup> SeedUserGroups()
    {
        var userGroups = new List<UserGroup>
            {
                new() {Id = Guid.Parse("96e1ec4d-8ae4-4714-980e-4e1effcdb8f9"), Name = "Admin"},
                new() {Id = Guid.Parse("96e1ec4d-8ae4-4714-981e-4e1effcdb8f9"), Name = "Memeber"},
                new() {Id = Guid.Parse("96e1ec4d-8ae4-4724-980e-4e1effcdb8f9"), Name = "Staff"}
            };

        return userGroups;
    }
    internal static List<Permission> SeedPermissions()
    {
        var permissions = new List<Permission>
        {
            new() {
                Id = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef851"),
                Name = PermissionEnum.WriteMember.ToString(),
            },
            new() {
                Id = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef852"),
                Name = PermissionEnum.ReadMember.ToString(),
            },
            new()
            {
                Id = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef853"),
                Name = PermissionEnum.DeleteMember.ToString(),
            },
            new()
            {
                Id = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef854"),
                Name = PermissionEnum.UpdateMember.ToString(),
            }
        };

        return permissions;
    }



}