using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class EmergencyContactEntityConfig: IEntityTypeConfiguration<EmergencyContact>
    {
        public void Configure(EntityTypeBuilder<EmergencyContact> builder)
        {
            builder.HasOne(ec => ec.Relationship)
                .WithMany(lk => lk.ECRelationships)
                .HasForeignKey(ec => ec.RelationshipId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(ec => ec.Applicant)
                .WithOne(appl => appl.EmergencyContact)
                .HasForeignKey<EmergencyContact>(d => d.ApplicantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ec => ec.Address)
                .WithOne(add => add.EmergencyContact)
                .HasForeignKey<EmergencyContact>(d => d.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }

    }
}