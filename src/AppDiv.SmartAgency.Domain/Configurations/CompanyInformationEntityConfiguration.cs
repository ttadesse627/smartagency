using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class CompanyInformationEntityConfiguration: IEntityTypeConfiguration<CompanyInformation>
    {
        public void Configure(EntityTypeBuilder<CompanyInformation> builder)
        {
            builder.HasOne(ci => ci.Address)
                .WithOne( a=> a.CompanyInformation)
                .HasForeignKey<CompanyInformation>(fk=> fk.AddressId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(ci => ci.CompanySetting)
                .WithOne( cs=> cs.CompanyInformation)
                .HasForeignKey<CompanySetting>(fk=> fk.CompanyInformationId)
                .OnDelete(DeleteBehavior.Cascade); 
            builder.HasMany(ci => ci.CountryOperations)
                .WithOne( co=> co.CompanyInformation)
                .HasForeignKey(fk=> fk.CompanyInformationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(ci => ci.Witnesses)
                .WithOne( w=> w.CompanyInformation)
                .HasForeignKey(fk=> fk.CompanyInformationId)
                .OnDelete(DeleteBehavior.Cascade);    
             
                
        }

    }
}