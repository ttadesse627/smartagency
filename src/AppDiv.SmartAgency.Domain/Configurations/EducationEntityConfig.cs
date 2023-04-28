using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EducationEntityConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasMany(edu => edu.LevelofEducationLookUps)
            .WithMany(lookup => lookup.LevelOfEducations);

        builder.HasOne(edu => edu.QualificationTypeLookUp)
            .WithMany(lk => lk.QualificationTypeEducations);

        builder.HasOne(edu => edu.AwardLookUp)
            .WithOne(lookup => lookup.AwardEducation)
            .HasForeignKey<Education>(fk => fk.AwardLookUpId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}