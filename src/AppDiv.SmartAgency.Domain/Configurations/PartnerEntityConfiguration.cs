using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class PartnerEntityConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasOne(p => p.Address)
                .WithOne(a => a.Partner)
                .HasForeignKey<Partner>(p => p.AddressId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
