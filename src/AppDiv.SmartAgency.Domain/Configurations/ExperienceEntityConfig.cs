using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class ExperienceEntityConfig: IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasOne(d => d.Applicant)
                .WithMany(a => a.Experiences)
                .HasForeignKey(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.HasOne(d => d.Country)
                .WithMany(a => a.ExpCountries)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.SetNull);
                
        }

    }
}