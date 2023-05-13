using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class AddressEntityConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(m => m.AddressRegion)
            .WithMany(n => n.LookUpAddressRegions)
            .HasForeignKey(fk => fk.AddressRegionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
