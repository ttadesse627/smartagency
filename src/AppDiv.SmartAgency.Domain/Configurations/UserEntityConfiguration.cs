﻿
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(user => user.Position)
                .WithMany(lk => lk.UserPositions)
                .HasForeignKey(fk => fk.PositionId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(user => user.Branch)
                .WithMany(lk => lk.UserBranchs)
                .HasForeignKey(fk => fk.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(user => user.Partner)
                .WithMany(lk => lk.Users)
                .HasForeignKey(fk => fk.PartnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(user => user.Address)
                .WithOne(addr => addr.ApplicationUser)
                .HasForeignKey<ApplicationUser>(fk => fk.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
