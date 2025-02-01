using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AppDiv.SmartAgency.Infrastructure.Seed
{
    internal class SeedData
    {
        internal static void Seedprocesses(ModelBuilder builder)
        {
            var staticProcess = new Process
            {
                Id = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86"),
                Name = "Ticket Process",
                Step = 100,
                VisaRequired = true,
                EnjazRequired = false
            };

            builder.Entity<Process>().HasData(staticProcess);
        }

        internal static void SeedprocessDefinitions(ModelBuilder builder)
        {
            var pDefs = new List<ProcessDefinition>
            {
                new() {Id = Guid.Parse("00fa1a8e-ac70-400e-8f37-20010f81a27a"), Name = "Ready to Issue Ticket", Step = 0, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("1dc479ab-fe84-4ca8-828f-9a21de7434e7"), Name = "Register Ticket", Step = 1, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("2d9ef769-6d03-4406-9849-430ff9723778"), Name = "Refund Ticket", Step = 2, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("3048b353-039d-41b6-8690-a9aaa2e679cf"), Name = "Rebook Ticket", Step = 3, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("4048b353-039d-41b6-8690-a9aaa2e679cf"), Name = "Register Rebook Ticket", Step = 4, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("5b912c00-9df3-47a1-a525-410abf239616"), Name = "Travel", Step = 5, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new() {Id = Guid.Parse("6b912c00-9df3-47a1-a524-410abf239616"), Name = "Traveled", Step = 6, RequestApproval = true, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") }
            };
            builder.Entity<ProcessDefinition>().HasData(pDefs);
        }

        internal static void SeedResources(ModelBuilder builder)
        {
            var resources = new List<Resource>
            {
                new() { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Group" },
                new() { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Lookup" },
                new() { Id = Guid.Parse("05050505-0505-0505-0505-050505050505"), Name = "Resource" },
                new() { Id = Guid.Parse("07070707-0707-0707-0707-070707070707"), Name = "User" },
            };

            builder.Entity<Resource>().HasData(resources);
        }

        internal static void SeedGroups(ModelBuilder builder)
        {
            var userGroupId = Guid.Parse("5a41ae30-1bd5-43ce-8e0d-28582b3eecd2");

            var userGroup = new UserGroup
            {
                Id = userGroupId,
                Name = "Admin"
            };

            builder.Entity<UserGroup>().HasData(userGroup);
        }

        internal static void SeedPermissions(ModelBuilder builder)
        {
            List<PermissionEnum> permissions =
            [
                PermissionEnum.Access,
                PermissionEnum.Write,
                PermissionEnum.Read,
                PermissionEnum.ReadDetail,
                PermissionEnum.Update,
                PermissionEnum.Delete
            ];

            var userGroupId = Guid.Parse("5a41ae30-1bd5-43ce-8e0d-28582b3eecd2");

            var permissionsList = new List<Permission>
    {
        new()
        {
            Id = Guid.Parse("55555555-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            ResourceId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            GroupId = userGroupId,
            Actions = permissions
        },
        new()
        {
            Id = Guid.Parse("66666666-cccc-cccc-cccc-cccccccccccc"),
            ResourceId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            GroupId = userGroupId,
            Actions = permissions
        },
        new()
        {
            Id = Guid.Parse("d4444444-0505-0505-0505-050505050505"),
            ResourceId = Guid.Parse("05050505-0505-0505-0505-050505050505"),
            GroupId = userGroupId,
            Actions = permissions
        },
        new()
        {
            Id = Guid.Parse("11111111-0707-0707-0707-070707070707"),
            ResourceId = Guid.Parse("07070707-0707-0707-0707-070707070707"),
            GroupId = userGroupId,
            Actions = permissions
        }
    };

            builder.Entity<Permission>().HasData(permissionsList);
        }
        internal static void SeedLookups(ModelBuilder builder)
        {
            var lookUps = new List<LookUp>
            {
                new() { Id = Guid.NewGuid(), Category = "Branch", Value = "Adama" },
                new() { Id = Guid.NewGuid(), Category = "Branch", Value = "Addis Ababa" },
                new() { Id = Guid.NewGuid(), Category = "Position", Value = "Help Desk" },
                new() { Id = Guid.NewGuid(), Category = "Position", Value = "Admin" },
                new() { Id = Guid.NewGuid(), Category = "Region", Value = "Oromia" },
                new() { Id = Guid.NewGuid(), Category = "Region", Value = "Addis Ababa" },
                new() { Id = Guid.NewGuid(), Category = "Region", Value = "Somali" },
                new() { Id = Guid.NewGuid(), Category = "Branch", Value = "Bahir Dar" },
                new() { Id = Guid.NewGuid(), Category = "Position", Value = "Secretary" },
            };

            builder.Entity<LookUp>().HasData(lookUps);
        }

    }
}
