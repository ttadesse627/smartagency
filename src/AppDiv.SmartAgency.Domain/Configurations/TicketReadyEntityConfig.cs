

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketReadyEntityConfig : IEntityTypeConfiguration<TicketReady>
{
    public void Configure(EntityTypeBuilder<TicketReady> builder)
    {
        builder.HasOne(pr => pr.Applicant)
            .WithOne(app => app.TicketReady)
            .HasForeignKey<TicketReady>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}