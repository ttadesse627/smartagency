using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class AddressEntityConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(m => m.AddressApplicant)
            .WithOne(n => n.Address)
            .HasForeignKey<Applicant>(fk => fk.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(add => add.AddressEmergContact)
                .WithOne(emc => emc.Address)
                .HasForeignKey<EmergencyContact>(fk => fk.AddressId);

        builder.HasOne(add => add.AddressRepresentative)
                .WithOne(emc => emc.Address)
                .HasForeignKey<Repersentative>(fk => fk.AddressId);
    }
}
