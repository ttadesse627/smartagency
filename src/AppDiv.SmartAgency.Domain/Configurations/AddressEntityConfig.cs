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
            .WithMany(n => n.AddressRegions)
            .HasForeignKey(fk => fk.AddressRegionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Country)
            .WithMany(l => l.Countries)
            .HasForeignKey(fk => fk.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
