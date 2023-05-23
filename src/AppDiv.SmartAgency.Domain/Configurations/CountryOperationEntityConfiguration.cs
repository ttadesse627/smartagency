using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class CountryOperationEntityConfiguration: IEntityTypeConfiguration<CountryOperation>
{
    public void Configure(EntityTypeBuilder<CountryOperation> builder)
    {
        builder.HasOne(co => co.LookUpCountryOperation)
            .WithOne(lk => lk.CountryOperation)
            .HasForeignKey<CountryOperation>(fk=> fk.CountryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
}