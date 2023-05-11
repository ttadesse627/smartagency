using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasMany(edu => edu.EducationLevelofQualifications)
            .WithMany(lk => lk.LookUpLevelOfQualifications);

        builder.HasMany(edu => edu.EducationQualificationTypes)
            .WithMany(lk => lk.LookUpQualificationTypes);

        builder.HasMany(edu => edu.EducationAwards)
            .WithMany(lk => lk.LookUpAwards);

        builder.HasOne(edu => edu.EducationApplicant)
            .WithOne(appl => appl.ApplicantEducation)
            .HasForeignKey<Education>(fk => fk.EducationApplicantId);
    }
}