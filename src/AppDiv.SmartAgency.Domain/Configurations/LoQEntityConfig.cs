using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class LoQEntityConfig : IEntityTypeConfiguration<LevelOfQualification>
{
    public void Configure(EntityTypeBuilder<LevelOfQualification> builder)
    {

        builder.HasOne(loq => loq.Education)
            .WithMany(edu => edu.LevelOfQualifications)
            .HasForeignKey(fk => fk.EducationId);

        builder.HasOne(loq => loq.LookUp)
            .WithMany(lk => lk.LevelOfQualifications)
            .HasForeignKey(fk => fk.LookUpId);

    }
}