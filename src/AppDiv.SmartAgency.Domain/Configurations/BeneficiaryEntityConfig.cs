

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class BeneficiaryEntityConfig : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasOne(ben => ben.Relationship)
            .WithMany(lk => lk.BenRelationShips)
            .HasForeignKey(fk => fk.RelationshipId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(ben => ben.Applicant)
            .WithMany(appl => appl.Beneficiaries)
            .HasForeignKey(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
