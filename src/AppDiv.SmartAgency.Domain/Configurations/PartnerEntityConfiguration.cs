using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class PartnerEntityConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasOne(p => p.PartnerAddress)
                .WithOne(a => a.Partner)
                .HasForeignKey<Partner>(p => p.PartnerAddressId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
