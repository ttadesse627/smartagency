using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class RepresentativeEntityConfig : IEntityTypeConfiguration<Representative>
{
    public void Configure(EntityTypeBuilder<Representative> builder)
    {
        builder.HasOne(m => m.RepresentativeApplicant)
            .WithOne(n => n.ApplicantRepresentative)
            .HasForeignKey<Representative>(fk => fk.RepresentativeApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.RepresentativeAddress)
            .WithOne(n => n.AddressRepresentative)
            .HasForeignKey<Representative>(fk => fk.RepresentativeAddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}