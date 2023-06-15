

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TraveledApplicantEntityConfig : IEntityTypeConfiguration<TraveledApplicant>
{
    public void Configure(EntityTypeBuilder<TraveledApplicant> builder)
    {
        builder.HasOne(pr => pr.ApplicantProcess)
            .WithOne(app => app.TraveledApplicant)
            .HasForeignKey<TraveledApplicant>(fk => fk.ApplicantProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}