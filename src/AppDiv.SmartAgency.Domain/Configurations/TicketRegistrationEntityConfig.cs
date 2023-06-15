

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketRegistrationEntityConfig : IEntityTypeConfiguration<TicketRegistration>
{
    public void Configure(EntityTypeBuilder<TicketRegistration> builder)
    {
        builder.HasOne(pr => pr.ApplicantProcess)
            .WithOne(app => app.TicketRegistration)
            .HasForeignKey<TicketRegistration>(fk => fk.ApplicantProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}