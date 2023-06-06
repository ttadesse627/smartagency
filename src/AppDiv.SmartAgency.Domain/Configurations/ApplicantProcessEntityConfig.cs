

using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ApplicantProcessEntityConfig : IEntityTypeConfiguration<ApplicantProcess>
{
    public void Configure(EntityTypeBuilder<ApplicantProcess> builder)
    {
        builder.HasOne(pr => pr.Applicant)
            .WithMany(app => app.ApplicantProcesses)
            .HasForeignKey(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pr => pr.Process)
            .WithMany(app => app.ApplicantProcesses)
            .HasForeignKey(fk => fk.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}