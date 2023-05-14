using AppDiv.SmartAgency.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class OrderEntityConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(m => m.Partner)
            .WithMany(n => n.PartnerOrders)
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

        builder.HasOne(m => m.Employee)
            .WithOne(n => n.ApplicantOrder)
            .HasForeignKey<Order>(fk => fk.EmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.OrderCriteria)
            .WithOne(n => n.Order)
            .HasForeignKey<Order>(fk => fk.OrderCriteriaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.OrderPayment)
            .WithOne(n => n.Order)
            .HasForeignKey<Order>(fk => fk.OrderPaymentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.OrderSponsor)
            .WithOne(n => n.SponsorOrder)
            .HasForeignKey<Order>(fk => fk.OrderSponsorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
