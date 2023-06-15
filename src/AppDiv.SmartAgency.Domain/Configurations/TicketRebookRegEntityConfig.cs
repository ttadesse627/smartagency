

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketRebookRegEntityConfig : IEntityTypeConfiguration<TicketRebookReg>
{
    public void Configure(EntityTypeBuilder<TicketRebookReg> builder)
    {
        builder.HasOne(pr => pr.ApplicantProcess)
            .WithOne(app => app.TicketRebookReg)
            .HasForeignKey<TicketRebookReg>(fk => fk.ApplicantProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}