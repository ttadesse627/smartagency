using AppDiv.SmartAgency.Domain.Entities;
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
                new ProcessDefinition {Id = Guid.Parse("00fa1a8e-ac70-400e-8f37-20010f81a27a"), Name = "Ready to Issue Ticket", Step = 0, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new ProcessDefinition {Id = Guid.Parse("1dc479ab-fe84-4ca8-828f-9a21de7434e7"), Name = "Register Ticket", Step = 1, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new ProcessDefinition {Id = Guid.Parse("2d9ef769-6d03-4406-9849-430ff9723778"), Name = "Refund Ticket", Step = 2, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new ProcessDefinition {Id = Guid.Parse("3048b353-039d-41b6-8690-a9aaa2e679cf"), Name = "Rebook Ticket", Step = 3, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new ProcessDefinition {Id = Guid.Parse("4048b353-039d-41b6-8690-a9aaa2e679cf"), Name = "Register Rebook Ticket", Step = 4, RequestApproval = false, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") },
                new ProcessDefinition {Id = Guid.Parse("5b912c00-9df3-47a1-a525-410abf239616"), Name = "Travel", Step = 2, RequestApproval = true, ProcessId = Guid.Parse("60209c9d-47b4-497b-8abd-94a753814a86") }
            };
            builder.Entity<ProcessDefinition>().HasData(pDefs);
        }
    }
}
