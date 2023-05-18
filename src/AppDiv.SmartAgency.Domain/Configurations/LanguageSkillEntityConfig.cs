using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class LanguageSkillEntityConfig : IEntityTypeConfiguration<LanguageSkill>
{
    public void Configure(EntityTypeBuilder<LanguageSkill> builder)
    {
        builder.HasOne(lan => lan.Language)
            .WithMany(lk => lk.LanguageSkills)
            .HasForeignKey(fk => fk.LanguageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Applicant)
            .WithMany(n => n.LanguageSkills)
            .HasForeignKey(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}