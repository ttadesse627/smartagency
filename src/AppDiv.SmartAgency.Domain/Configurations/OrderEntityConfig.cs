using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class OrderEntityConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(m => m.Partner)
            .WithMany(n => n.Orders)
            .HasForeignKey(fk => fk.PartnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.PortOfArrival)
            .WithMany(n => n.LookUpPortOfArrivals)
            .HasForeignKey(fk => fk.PortOfArrivalId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Priority)
            .WithMany(n => n.LookUpPriorities)
            .HasForeignKey(fk => fk.PriorityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.VisaType)
            .WithMany(n => n.LookUpVisaTypes)
            .HasForeignKey(fk => fk.VisaTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.Payment)
            .WithOne(n => n.Order)
            .HasForeignKey<Payment>(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Sponsor)
            .WithOne(n => n.Order)
            .HasForeignKey<Sponsor>(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Attachment)
            .WithMany(n => n.Orders)
            .HasForeignKey(fk => fk.AttachmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
