using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class AddressEntityConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne(a => a.Country)
            .WithMany(l => l.Countries)
            .HasForeignKey(fk => fk.CountryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Region)
            .WithMany(n => n.Regions)
            .HasForeignKey(fk => fk.RegionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.City)
            .WithMany(n => n.Cities)
            .HasForeignKey(fk => fk.CityId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
