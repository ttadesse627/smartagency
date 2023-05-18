using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppDiv.SmartAgency.Domain.Entities;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class PartnerEntityConfig : IEntityTypeConfiguration<Partner>
{
    public void Configure(EntityTypeBuilder<Partner> builder)
    {
        builder.HasOne(m => m.Address)
            .WithOne(n => n.Partner)
            .HasForeignKey<Partner>(fk => fk.AddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
