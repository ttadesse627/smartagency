using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class RepresentativeEntityConfig : IEntityTypeConfiguration<Repersentative>
{
    public void Configure(EntityTypeBuilder<Repersentative> builder)
    {
        builder.HasOne(m => m.RepresentativeApplicant)
            .WithOne(n => n.ApplicantRepersentative)
            .HasForeignKey<Repersentative>(fk => fk.RepresentativeApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.RepersentativeAddress)
            .WithOne(n => n.AddressRepresentative)
            .HasForeignKey<Repersentative>(fk => fk.RepersentativeAddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}