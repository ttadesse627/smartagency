using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasMany(edu => edu.EducationLevelofQualifications)
            .WithOne(loq => loq.LevelOfQualificationEducation)
            .HasForeignKey(fk => fk.LevelOfQualificationEducationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(edu => edu.EducationQualificationTypes)
            .WithOne(qt => qt.QualificationTypeEducation)
            .HasForeignKey(fk => fk.QualificationTypeEducationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(edu => edu.EducationAawards)
            .WithOne(aw => aw.AwardEducation)
            .HasForeignKey(fk => fk.AwardEducationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(edu => edu.EducationApplicant)
            .WithOne(appl => appl.ApplicantEducation)
            .HasForeignKey<Education>(fk => fk.EducationApplicantId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}