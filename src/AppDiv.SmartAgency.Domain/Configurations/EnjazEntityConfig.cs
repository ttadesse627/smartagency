
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EnjazEntityConfig : IEntityTypeConfiguration<Enjaz>
{
    public void Configure(EntityTypeBuilder<Enjaz> builder)
    {
        builder.HasIndex(enj => enj.ApplicationNumber)
            .IsUnique();

        builder.HasIndex(enj => enj.OrderId)
            .IsUnique();

        builder.HasOne(enj => enj.Order)
            .WithOne(sp => sp.Enjaz)
            .HasForeignKey<Enjaz>(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}