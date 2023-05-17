using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class OrderEntityConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(m => m.OrderPartner)
            .WithMany(n => n.PartnerOrders)
            .HasForeignKey(fk => fk.OrderPartnerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderPortOfArrival)
            .WithMany(n => n.LookUpPortOfArrivals)
            .HasForeignKey(fk => fk.OrderPortOfArrivalId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderPriority)
            .WithMany(n => n.LookUpPriorities)
            .HasForeignKey(fk => fk.OrderPriorityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderVisaType)
            .WithMany(n => n.LookUpVisaTypes)
            .HasForeignKey(fk => fk.OrderVisaTypeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderEmployee)
            .WithOne(n => n.ApplicantOrder)
            .HasForeignKey<Order>(fk => fk.OrderEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderPayment)
            .WithOne(n => n.PaymentOrder)
            .HasForeignKey<Payment>(fk => fk.PaymentOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.OrderSponsor)
            .WithOne(n => n.SponsorOrder)
            .HasForeignKey<Sponsor>(fk => fk.SponsorOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
