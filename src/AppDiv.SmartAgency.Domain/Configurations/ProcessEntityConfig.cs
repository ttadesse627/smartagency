

using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ProcessEntityConfig : IEntityTypeConfiguration<Process>
{
    public void Configure(EntityTypeBuilder<Process> builder)
    {
        builder.HasOne(pr => pr.Country)
            .WithMany(lk => lk.ProcessCountries)
            .HasForeignKey(fk => fk.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}