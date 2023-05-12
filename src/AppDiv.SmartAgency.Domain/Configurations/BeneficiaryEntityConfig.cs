

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class BeneficiaryEntityConfig : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasOne(ben => ben.BeneficiaryRelationship)
            .WithMany(lk => lk.BeneficiaryRelationShip)
            .HasForeignKey(fk => fk.BeneficiaryRelationshipId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(bnf => bnf.BeneficiaryApplicant)
            .WithMany(appl => appl.ApplicantBeneficiaries)
            .HasForeignKey(fk => fk.BeneficiaryApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
