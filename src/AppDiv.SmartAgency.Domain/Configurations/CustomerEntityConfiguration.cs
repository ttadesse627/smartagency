using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(s => s.CreatedBy)
                .HasDefaultValue(string.Empty);
            builder.Property(s => s.ModifiedBy)
                .HasDefaultValue(string.Empty);

            // builder.HasOne(m => m.Gender)
            //     .WithMany()
            //     .HasForeignKey(u => u.GenderId)
            //     .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Suffix)
                .WithMany()
                .HasForeignKey(u => u.SuffixId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
