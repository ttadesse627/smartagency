

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketRebookEntityConfig : IEntityTypeConfiguration<TicketRebook>
{
    public void Configure(EntityTypeBuilder<TicketRebook> builder)
    {
        builder.HasOne(pr => pr.Applicant)
            .WithOne(app => app.TicketRebook)
            .HasForeignKey<TicketRebook>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}