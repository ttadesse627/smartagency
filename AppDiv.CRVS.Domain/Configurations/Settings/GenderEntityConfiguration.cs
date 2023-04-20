using AppDiv.CRVS.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ab.Domain.Configuration.Settings
{
    public class GenderEntityConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Name, "IDX_UQ_Gender").IsUnique();
            builder.Property(m => m.CreatedAt).HasDefaultValue(DateTime.Now);
        }
    }
}
