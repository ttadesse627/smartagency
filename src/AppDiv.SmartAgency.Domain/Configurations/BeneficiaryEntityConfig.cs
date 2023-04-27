

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class BeneficiaryEntityConfig : IEntityTypeConfiguration<Beneficiary>
{
    public void Configure(EntityTypeBuilder<Beneficiary> builder)
    {
        builder.HasOne(ben => ben.Relationship)
                .WithOne()
                .HasForeignKey<Beneficiary>(b => b.RelationshipId)
                .OnDelete(DeleteBehavior.NoAction);
    }
}
