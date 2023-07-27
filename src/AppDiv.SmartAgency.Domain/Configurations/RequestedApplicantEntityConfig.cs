using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class RequestedApplicantEntityConfig : IEntityTypeConfiguration<RequestedApplicant>
{
    public void Configure(EntityTypeBuilder<RequestedApplicant> builder)
    {
        builder.HasOne(a => a.Applicant)
            .WithOne(l => l.RequestedApplicant)
            .HasForeignKey<RequestedApplicant>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Partner)
            .WithMany(n => n.RequestedApplicants)
            .HasForeignKey(fk => fk.PartnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
