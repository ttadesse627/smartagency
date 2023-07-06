
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class ComplaintEntityConfig : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            builder.HasOne(comp => comp.User)
                .WithMany(user => user.Complaints)
                .HasForeignKey(fk => fk.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(comp => comp.Applicant)
                .WithMany(ord => ord.Complaints)
                .HasForeignKey(fk => fk.ApplicantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
