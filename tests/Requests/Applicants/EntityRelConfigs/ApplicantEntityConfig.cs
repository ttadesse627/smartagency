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

        builder.HasMany(m => m.ApplicantWitnesses)
            .WithMany(n => n.WitnessApplicants);

        builder.HasOne(m => m.ApplicantPartner)
            .WithMany(n => n.PartnerApplicants)
            .HasForeignKey(fk => fk.ApplicantPartnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.ApplicantAddress)
            .WithOne(n => n.AddressApplicant)
            .HasForeignKey<Applicant>(fk => fk.ApplicantAddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.ApplicantBankAccount)
            .WithOne(n => n.Applicant)
            .HasForeignKey<Applicant>(n => n.ApplicantBankAccountId)
            .OnDelete(DeleteBehavior.Cascade);

            // Relationships with lookup

        builder.HasMany(m => m.ApplicantTechnicalSkills)
            .WithMany(n => n.LookUpTechnicalSkills);

        builder.HasOne(m => m.ApplicantExprience)
            .WithMany(n => n.LookUpExpriences)
            .HasForeignKey(fk => fk.ApplicantExprienceId)
            .OnDelete(DeleteBehavior.SetNull);

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

        builder.HasOne(appl => appl.ApplicantIssuingCountry)
            .WithMany(lk => lk.LookUpIssuingCountries)
            .HasForeignKey(fk => fk.ApplicantIssuingCountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantIssuedPlace)
            .WithMany(lk => lk.LookUpIssuedPlaces)
            .HasForeignKey(fk => fk.ApplicantIssuedPlaceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantHealth)
            .WithMany(lk => lk.LookUpHealths)
            .HasForeignKey(fk => fk.ApplicantHealthId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantSalary)
            .WithMany(lk => lk.LookUpSalaries)
            .HasForeignKey(fk => fk.ApplicantSalaryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantDesiredCountry)
            .WithMany(lk => lk.LookUpDesiredCountries)
            .HasForeignKey(fk => fk.ApplicantDesiredCountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.ApplicantMaritalStatus)
            .WithMany(lk => lk.LookUpMaritalStatuses)
            .HasForeignKey(fk => fk.ApplicantMaritalStatusId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}