using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class OrderCriteriaEntityConfig : IEntityTypeConfiguration<OrderCriteria>
{
    public void Configure(EntityTypeBuilder<OrderCriteria> builder)
    {
        builder.HasOne(m => m.Nationality)
            .WithMany(n => n.LookUpCriteriaNationalities)
            .HasForeignKey(fk => fk.NationalityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaJobTitle)
            .WithMany(n => n.LookUpCriteriaJobTitles)
            .HasForeignKey(fk => fk.OrderCriteriaJobTitleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Salary)
            .WithMany(n => n.LookUpCriteriaSalaries)
            .HasForeignKey(fk => fk.SalaryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Religion)
            .WithMany(n => n.LookUpCriteriaReligions)
            .HasForeignKey(fk => fk.ReligionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Experience)
            .WithMany(n => n.LookUpCriteriaExperiences)
            .HasForeignKey(fk => fk.ExperienceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Language)
            .WithMany(n => n.LookUpCriteriaLanguages)
            .HasForeignKey(fk => fk.LanguageId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
