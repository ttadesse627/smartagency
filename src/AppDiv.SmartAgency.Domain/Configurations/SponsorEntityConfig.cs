using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class SponsorEntityConfig : IEntityTypeConfiguration<Sponsor>
{
    public void Configure(EntityTypeBuilder<Sponsor> builder)
    {
        builder.HasOne(m => m.SponsorIDFile)
            .WithOne(n => n.FileCollectionSponsor)
            .HasForeignKey<Sponsor>(fk => fk.SponsorIDFileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.SponsorAddress)
            .WithOne(n => n.AddressSponsor)
            .HasForeignKey<Sponsor>(fk => fk.SponsorAddressId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
