using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ApplicantEntityConfig : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {

        builder.HasIndex(appl => appl.PassportNumber)
            .IsUnique();

        builder.Property(appl => appl.PassportNumber)
            .IsRequired();

        builder.HasMany(appl => appl.Witnesses)
            .WithOne(wt => wt.Applicant)
            .HasForeignKey(wt => wt.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(appl => appl.BankAccount)
            .WithOne(ls => ls.Applicant)
            .HasForeignKey<BankAccount>(ba => ba.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(appl => appl.Representative)
            .WithOne(rep => rep.Applicant)
            .HasForeignKey<Representative>(rep => rep.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Address)
            .WithOne(n => n.Applicant)
            .HasForeignKey<Applicant>(fk => fk.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(appl => appl.Partner)
            .WithMany(rep => rep.Applicants)
            .HasForeignKey(appl => appl.PartnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Order)
            .WithMany(rep => rep.Employees)
            .HasForeignKey(appl => appl.OrderId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.IssuingCountry)
            .WithMany(lk => lk.ApplIssuingCountries)
            .HasForeignKey(appl => appl.IssuingCountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.PassportIssuedPlace)
            .WithMany(lk => lk.ApplPassportIssuedPlaces)
            .HasForeignKey(appl => appl.PassportIssuedPlaceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.MaritalStatus)
            .WithMany(lk => lk.ApplMaritalStatuses)
            .HasForeignKey(appl => appl.MaritalStatusId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Health)
            .WithMany(lk => lk.ApplHealthes)
            .HasForeignKey(appl => appl.HealthId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Religion)
            .WithMany(lk => lk.ApplReligions)
            .HasForeignKey(appl => appl.ReligionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Jobtitle)
            .WithMany(lk => lk.ApplJobtitles)
            .HasForeignKey(appl => appl.JobtitleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Experience)
            .WithMany(lk => lk.ApplExperiences)
            .HasForeignKey(appl => appl.ExperienceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Language)
            .WithMany(lk => lk.ApplLanguages)
            .HasForeignKey(appl => appl.LanguageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Salary)
            .WithMany(lk => lk.ApplSalaries)
            .HasForeignKey(appl => appl.SalaryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.DesiredCountry)
            .WithMany(lk => lk.ApplDesiredCountries)
            .HasForeignKey(appl => appl.DesiredCountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.BrokerName)
            .WithMany(lk => lk.ApplBrokerNames)
            .HasForeignKey(appl => appl.BrokerNameId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.Branch)
            .WithMany(lk => lk.ApplBranches)
            .HasForeignKey(appl => appl.BranchId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(appl => appl.CurrentNationality)
            .WithMany(lk => lk.ApplCurrentNationalities)
            .HasForeignKey(appl => appl.CurrentNationalityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(appl => appl.Attachments)
            .WithMany(att => att.Applicants);
    }
}
