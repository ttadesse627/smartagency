using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
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
            .WithMany(n => n.Applicants)
            .HasForeignKey(n => n.PartnerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(m => m.Languages)
            .WithMany(n => n.Applicants);

        builder.HasMany(m => m.TechnicalSkills)
            .WithMany(n => n.TechnicalSkillApplicants)
            .UsingEntity<TechnicalSkill>();
        
        builder.HasMany(m => m.Experiences)
            .WithOne(n => n.Applicant)
            .HasForeignKey(n => n.ApplicantId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(m => m.BankAccount)
            .WithOne(n => n.Applicant)
            .HasForeignKey<BankAccount>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Witnesses)
            .WithMany(n => n.Applicants);

        builder.HasMany(m => m.Beneficiaries)
            .WithOne(n => n.Applicant)
            .HasForeignKey(n => n.ApplicantId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(m => m.AttachmentFiles)
            .WithOne(n => n.ApplicantAttachmentFile)
            .HasForeignKey(n => n.ApplicantId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(m => m.EmergencyContact)
            .WithOne(n => n.Applicant)
            .HasForeignKey<EmergencyContact>(n => n.ApplicantId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(m => m.Repersentative)
            .WithMany(n => n.RepresentativeApplicants)
            .HasForeignKey(n => n.RepersentativeId)
            .OnDelete(DeleteBehavior.ClientCascade);

            
    }
}