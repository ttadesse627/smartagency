using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class SponsorEntityConfig : IEntityTypeConfiguration<Sponsor>
{
    public void Configure(EntityTypeBuilder<Sponsor> builder)
    {
        builder.HasOne(m => m.Address)
            .WithOne(n => n.Sponsor)
            .HasForeignKey<Sponsor>(fk => fk.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Attachment)
            .WithMany(n => n.Sponsors)
            .HasForeignKey(fk => fk.AttachmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
