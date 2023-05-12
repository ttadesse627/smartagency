
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class EmergencyContactEntityConfig : IEntityTypeConfiguration<EmergencyContact>
{
    public void Configure(EntityTypeBuilder<EmergencyContact> builder)
    {
        builder.HasOne(m => m.EmergencyContactApplicant)
            .WithOne(n => n.ApplicantEmergencyContact)
            .HasForeignKey<EmergencyContact>(n => n.EmergencyContactApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.EmergencyContactRegion)
            .WithMany(n => n.LookUpEmergencyContactRegions)
            .HasForeignKey(n => n.EmergencyContactRegionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.EmergencyContactRelationship)
            .WithMany(n => n.LookUpEmergencyContactRelationships)
            .HasForeignKey(n => n.EmergencyContactRelationshipId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(m => m.EmergencyContactAddress)
            .WithOne(n => n.AddressEmergencyContact)
            .HasForeignKey<EmergencyContact>(n => n.EmergencyContactAddressId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}