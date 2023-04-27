using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ApplicantEntityConfig : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.Property(s => s.CreatedBy)
            .HasDefaultValue(string.Empty);
            
        builder.Property(s => s.ModifiedBy)
            .HasDefaultValue(string.Empty);

        builder.HasOne(m => m.Religion)
            .WithMany(n => n.ApplicantReligions)
            .HasForeignKey(u => u.ReligionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(m => m.LookUpJobTitles)
            .WithMany(n => n.ApplicantJobTitles);

        builder.HasOne(m => m.Partner)
            .WithMany()
            .HasForeignKey(n => n.PartnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(m => m.Languages)
            .WithMany(n => n.Applicants);
    }
}