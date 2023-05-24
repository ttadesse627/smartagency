using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class QTEntityConfig : IEntityTypeConfiguration<QualificationType>
{
    public void Configure(EntityTypeBuilder<QualificationType> builder)
    {

        builder.HasOne(qt => qt.Education)
            .WithMany(edu => edu.QualificationTypes)
            .HasForeignKey(fk => fk.EducationId);

        builder.HasOne(qt => qt.LookUp)
            .WithMany(lk => lk.QualificationTypes)
            .HasForeignKey(fk => fk.LookUpId);

    }
}