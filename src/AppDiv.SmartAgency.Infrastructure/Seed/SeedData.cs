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
                new() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Applicant" },
                new() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "ApplicantFollowupStatus" },
                new() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "ApplProcess" },
                new() { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Attachment" },
                new() { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "CompanyInformation" },
                new() { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Complaint" },
                new() { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Dashboard" },
                new() { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "DeletedInfo" },
                new() { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Deposit" },
                new() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Enjaz" },
                new() { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Group" },
                new() { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Lookup" },
                new() { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "OnlineApplicant" },
                new() { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Order" },
                new() { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "Page" },
                new() { Id = Guid.Parse("01010101-0101-0101-0101-010101010101"), Name = "PostImage" },
                new() { Id = Guid.Parse("02020202-0202-0202-0202-020202020202"), Name = "Process" },
                new() { Id = Guid.Parse("03030303-0303-0303-0303-030303030303"), Name = "QuickLink" },
                new() { Id = Guid.Parse("04040404-0404-0404-0404-040404040404"), Name = "Report" },
                new() { Id = Guid.Parse("05050505-0505-0505-0505-050505050505"), Name = "Resource" },
                new() { Id = Guid.Parse("06060606-0606-0606-0606-060606060606"), Name = "TicketProcess" },
                new() { Id = Guid.Parse("07070707-0707-0707-0707-070707070707"), Name = "User" },
            };

            builder.Entity<Resource>().HasData(resources);
        }
        internal static void SeedGroups(ModelBuilder builder)
        {
            var resources = new List<UserGroup>
            {
                new()
                {
                    Id = Guid.Parse("5a41ae30-1bd5-43ce-8e0d-28582b3eecd2"),
                    Name = "Admin",
                    Permissions =
                    [   new Permission {
                            Id = Guid.Parse("ea579b1a-0de7-41ce-88da-3ce735b8cf2d"),
                            ResourceId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                            Actions = [PermissionEnum.Access,PermissionEnum.Write,PermissionEnum.Read, PermissionEnum.ReadDetail,PermissionEnum.Update,PermissionEnum.Delete]
                        }
                    ]
                },
            };

            builder.Entity<Resource>().HasData(resources);
        }

    }
}
