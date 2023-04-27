using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasOne(edu => edu.LevelofEducation)
            .WithOne(lookup => lookup.LevelOfEducation);

        builder.HasOne(edu => edu.QualificationType)
            .WithOne(lookup => lookup.QualificationType);

        builder.HasMany(edu => edu.Awards)
            .WithMany(lookup => lookup.Awards);
    }
}