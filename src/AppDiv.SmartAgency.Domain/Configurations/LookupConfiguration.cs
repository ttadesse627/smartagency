using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class LookUpEntityConfiguration : IEntityTypeConfiguration<LookUp>
    {
        public void Configure(EntityTypeBuilder<LookUp> builder)
        {
            
            builder.HasOne(m => m.CategoryId)
                .WithMany(n => n.)
                .HasForeignKey(u => u.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
