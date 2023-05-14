using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class FileCollectionEntityConfig : IEntityTypeConfiguration<FileCollection>
{
    public void Configure(EntityTypeBuilder<FileCollection> builder)
    {
        builder.HasOne(fc => fc.FileCollectionAttachment)
            .WithMany(att => att.FileCollections)
            .HasForeignKey(fk => fk.FileCollectionAttachmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fc => fc.FileCollectionApplicant)
            .WithMany(appl => appl.ApplicantFileCollections)
            .HasForeignKey(fk => fk.FileCollectionApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.FileCollectionOrder)
            .WithOne(n => n.VisaFile)
            .HasForeignKey<FileCollection>(fk => fk.FileCollectionOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
