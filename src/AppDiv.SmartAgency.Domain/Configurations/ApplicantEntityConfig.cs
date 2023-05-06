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

        builder.HasMany(m => m.ApplicantWitnesses)
            .WithMany(n => n.WitnessApplicants);

        builder.HasMany(m => m.ApplicantBeneficiaries)
            .WithOne(n => n.BeneficiaryApplicant)
            .HasForeignKey(fk => fk.BeneficiaryApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.ApplicantLanguages)
            .WithOne(n => n.LanguageApplicant)
            .HasForeignKey(fk => fk.LanguageApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.ApplicantExperiences)
            .WithOne(n => n.ExperienceApplicant)
            .HasForeignKey(fk => fk.ExperienceApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ApplicantRepersentative)
            .WithOne(n => n.RepresentativeApplicant)
            .HasForeignKey<Repersentative>(n => n.RepresentativeApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ApplicantPartner)
            .WithMany(n => n.Applicants)
            .HasForeignKey(fk => fk.ApplicantPartnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.ApplicantAddress)
            .WithOne(n => n.AddressApplicant);

        builder.HasOne(m => m.ApplicantEducation)
            .WithOne(n => n.EducationApplicant)
            .HasForeignKey<Education>(n => n.EducationApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ApplicantBankAccount)
            .WithOne(n => n.Applicant)
            .HasForeignKey<BankAccount>(n => n.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ApplicantEmergencyContact)
            .WithOne(n => n.EmergencyContactApplicant)
            .HasForeignKey<EmergencyContact>(n => n.EmergencyContactApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.ApplicantTechnicalSkills)
            .WithMany(n => n.LookupTechnicalSkills);

        builder.HasOne(appl => appl.ApplicantReligion)
            .WithMany(lk => lk.LookUpReligions)
            .HasForeignKey(fk => fk.ApplicantReligionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantBrokerName)
            .WithMany(lk => lk.LookUpBrokerNames)
            .HasForeignKey(fk => fk.ApplicantBrokerNameId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantBranch)
            .WithMany(lk => lk.LookUpBranches)
            .HasForeignKey(fk => fk.ApplicantBranchId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantJobtitle)
            .WithMany(lk => lk.LookUpJobTitles)
            .HasForeignKey(fk => fk.ApplicantJobtitleId)
            .OnDelete(DeleteBehavior.SetNull);


    }
}