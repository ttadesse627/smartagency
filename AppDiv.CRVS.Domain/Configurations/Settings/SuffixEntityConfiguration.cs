
using AppDiv.CRVS.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.CRVS.Domain.Configuration.Settings
{
    public class SuffixEntityConfiguration : IEntityTypeConfiguration<Suffix>
    {
        public void Configure(EntityTypeBuilder<Suffix> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Name, "IDX_UQ_Suffix").IsUnique();
            builder.Property(m => m.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
