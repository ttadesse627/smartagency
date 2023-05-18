using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {

        builder.HasOne(edu => edu.Applicant)
            .WithOne(appl => appl.Education)
            .HasForeignKey<Education>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.QualificationTypes)
                .WithMany(l => l.EduQualificationTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "QualificationTypes",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

        builder.HasMany(e => e.LevelofQualifications)
                .WithMany(l => l.EduLevelOfQualifications)
                .UsingEntity<Dictionary<string, object>>(
                    "LevelOfQualifications",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

        builder.HasMany(e => e.Awards)
                .WithMany(l => l.EduAwards)
                .UsingEntity<Dictionary<string, object>>(
                    "Awards",
                    j => j.HasOne<LookUp>().WithMany().HasForeignKey("LookUpId"),
                    j => j.HasOne<Education>().WithMany().HasForeignKey("EducationId")
                );

    }
}