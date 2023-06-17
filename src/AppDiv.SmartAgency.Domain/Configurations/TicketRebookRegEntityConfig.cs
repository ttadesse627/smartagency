

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketRebookRegEntityConfig : IEntityTypeConfiguration<TicketRebookReg>
{
    public void Configure(EntityTypeBuilder<TicketRebookReg> builder)
    {
        builder.HasOne(pr => pr.Applicant)
            .WithOne(app => app.TicketRebookRegistration)
            .HasForeignKey<TicketRebookReg>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}