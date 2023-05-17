using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {

        builder.HasOne(edu => edu.EducationApplicant)
            .WithOne(appl => appl.ApplicantEducation)
            .HasForeignKey<Education>(fk => fk.EducationApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.EducationQualificationTypes)
                .WithMany(l => l.LookUpQualificationTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "EducationQualificationTypes",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

        builder.HasMany(e => e.EducationLevelofQualifications)
                .WithMany(l => l.LookUpLevelOfQualifications)
                .UsingEntity<Dictionary<string, object>>(
                    "EducationLevelOfQualifications",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

        builder.HasMany(e => e.EducationAwards)
                .WithMany(l => l.LookUpAwards)
                .UsingEntity<Dictionary<string, object>>(
                    "EducationAwards",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

    }
}