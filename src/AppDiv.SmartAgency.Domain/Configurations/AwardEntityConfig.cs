using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class AwardEntityConfig : IEntityTypeConfiguration<Award>
{
    public void Configure(EntityTypeBuilder<Award> builder)
    {

        builder.HasOne(aw => aw.Education)
            .WithMany(edu => edu.Awards)
            .HasForeignKey(fk => fk.EducationId);

        builder.HasOne(aw => aw.LookUp)
            .WithMany(lk => lk.Awards)
            .HasForeignKey(fk => fk.LookUpId);

    }
}