using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ExperienceEntityConfig : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.HasOne(m => m.ExperienceApplicant)
            .WithMany(n => n.ApplicantExperiences)
            .HasForeignKey(fk => fk.ExperienceApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ExperienceCountry)
            .WithMany(n => n.LookUpExperiences)
            .HasForeignKey(fk => fk.ExperienceCountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
