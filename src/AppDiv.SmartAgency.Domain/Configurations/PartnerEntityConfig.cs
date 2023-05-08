using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class PartnerEntityConfig : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.HasOne(m => m.PartnerAddress)
            .WithOne(n => n.AddressPartner)
            .HasForeignKey<Partner>(fk => fk.PartnerAddressId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
