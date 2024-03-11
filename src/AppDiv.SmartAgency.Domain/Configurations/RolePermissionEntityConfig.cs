using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class RolePermissionEntityConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(rp => new { rp.UserGroupId, rp.PermissionId });
        builder.HasData(SeedUserGroupPermissions());
    }

    private static List<RolePermission> SeedUserGroupPermissions()
    {
        var rolePermissions = new List<RolePermission>
        {
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef851") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef852") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef853") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef854") },

            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-981e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef851") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-981e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef852") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-981e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef853") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4714-981e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef854") },

            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4724-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef851") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4724-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef852") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4724-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef853") },
            new() { UserGroupId = Guid.Parse("96e1ec4d-8ae4-4724-980e-4e1effcdb8f9"), PermissionId = Guid.Parse("062bf23f-7926-4398-8cd9-c29bfd9ef854") },
        };

        return rolePermissions;
    }
}