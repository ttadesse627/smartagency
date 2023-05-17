using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class OrderCriteriaEntityConfig : IEntityTypeConfiguration<OrderCriteria>
{
    public void Configure(EntityTypeBuilder<OrderCriteria> builder)
    {
        builder.HasOne(m => m.OrderCriteriaNationality)
            .WithMany(n => n.LookUpCriteriaNationalities)
            .HasForeignKey(fk => fk.OrderCriteriaNationalityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaJobTitle)
            .WithMany(n => n.LookUpCriteriaJobTitles)
            .HasForeignKey(fk => fk.OrderCriteriaJobTitleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaSalary)
            .WithMany(n => n.LookUpCriteriaSalaries)
            .HasForeignKey(fk => fk.OrderCriteriaSalaryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaReligion)
            .WithMany(n => n.LookUpCriteriaReligions)
            .HasForeignKey(fk => fk.OrderCriteriaReligionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaExperience)
            .WithMany(n => n.LookUpCriteriaExperiences)
            .HasForeignKey(fk => fk.OrderCriteriaExperienceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaLanguage)
            .WithMany(n => n.LookUpCriteriaLanguages)
            .HasForeignKey(fk => fk.OrderCriteriaLanguageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteriaOrder)
            .WithOne(n => n.OrderCriteria)
            .HasForeignKey<OrderCriteria>(fk => fk.OrderCriteriaOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
