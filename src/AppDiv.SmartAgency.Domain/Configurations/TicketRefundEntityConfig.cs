

using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.TicketData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class TicketRefundEntityConfig : IEntityTypeConfiguration<TicketRefund>
{
    public void Configure(EntityTypeBuilder<TicketRefund> builder)
    {
        builder.HasOne(pr => pr.Applicant)
            .WithOne(app => app.TicketRefund)
            .HasForeignKey<TicketRefund>(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}