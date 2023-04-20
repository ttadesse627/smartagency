using AppDiv.CRVS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.CRVS.Domain.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {     
            builder.Property(s => s.CreatedBy)
                .HasDefaultValue(Guid.Empty);
            builder.Property(s => s.ModifiedBy)
                .HasDefaultValue(Guid.Empty);

            builder.HasOne(m => m.Gender)
               .WithMany()
               .HasForeignKey(u => u.GenderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Suffix)
               .WithMany()
                 .HasForeignKey(u => u.SuffixId)
               .OnDelete(DeleteBehavior.NoAction);
        }
       
    }
}
