using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class AttachmentFileEntityConfig : IEntityTypeConfiguration<AttachmentFile>
{
    public void Configure(EntityTypeBuilder<AttachmentFile> builder)
    {
        builder.HasOne(fc => fc.Attachment)
            .WithMany(att => att.AttachmentFiles)
            .HasForeignKey(fk => fk.AttachmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(fc => fc.Applicant)
            .WithMany(appl => appl.AttachmentFiles)
            .HasForeignKey(fk => fk.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(m => m.Order)
            .WithOne(n => n.AttachmentFile)
            .HasForeignKey<AttachmentFile>(fk => fk.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
