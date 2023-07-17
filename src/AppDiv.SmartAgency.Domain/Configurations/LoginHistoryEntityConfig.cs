

using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class LoginHistoryEntityConfig : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
        builder.HasOne(hist => hist.User)
            .WithMany(user => user.LoginHistories)
            .HasForeignKey(hist => hist.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}