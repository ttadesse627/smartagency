using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class SkillEntityConfig : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {

        builder.HasOne(sk => sk.Applicant)
            .WithMany(appl => appl.Skills)
            .HasForeignKey(fk => fk.ApplicantId);

        builder.HasOne(sk => sk.LookUp)
            .WithMany(lk => lk.Skills)
            .HasForeignKey(fk => fk.LookUpId);

    }
}