

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TechSkillEntityConfig : IEntityTypeConfiguration<TechnicalSkill>
{
    public void Configure(EntityTypeBuilder<TechnicalSkill> builder)
    {
        builder.HasOne(tech => tech.ApplicantTechnicalSkill)
                .WithMany(app => app.TechnicalSkills);
    }
}
