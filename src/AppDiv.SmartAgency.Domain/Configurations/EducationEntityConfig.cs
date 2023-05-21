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

        builder.HasMany(edu => edu.QualificationTypes)
            .WithMany(appl => appl.QualificationTypes);

        builder.HasMany(edu => edu.LevelOfQualifications)
            .WithMany(lk => lk.LevelOfQualifications);

        builder.HasMany(edu => edu.Awards)
            .WithMany(lk => lk.Awards);

    }
}