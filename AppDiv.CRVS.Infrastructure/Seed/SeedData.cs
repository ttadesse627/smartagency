using AppDiv.CRVS.Domain;
using AppDiv.CRVS.Domain;
using AppDiv.CRVS.Domain.Entities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDiv.CRVS.Infrastructure.Seed
{
    internal class SeedData
    {
        internal static void SeedUsers(ModelBuilder builder)
        {
        //     ApplicationUser user = new ApplicationUser()
        //     {
        //         Id = "b74ddd14-6340-4840-95c2-db12554843e5",
        //         UserName = "Admin",
        //         NormalizedUserName = "ADMIN",
        //         Email = "admin@gmail.com",
        //         NormalizedEmail = "ADMIN@GMAIL.COM",
        //         LockoutEnabled = false,
        //         PhoneNumber = "1234567890"
        //     };

        //     var passwordHasher = new PasswordHasher<ApplicationUser>();
        //     user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123");

        //     builder.Entity<ApplicationUser>().HasData(user);
        // }

        // internal static void SeedRoles(ModelBuilder builder)
        // {
        //     builder.Entity<IdentityRole>().HasData(
        //         new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //         new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "HR", ConcurrencyStamp = "2", NormalizedName = "Human Resource" }
        //         );
        // }

        // internal static void SeedUserRoles(ModelBuilder builder)
        // {
        //     builder.Entity<IdentityUserRole<string>>().HasData(
        //         new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
        //         );
        // }

        // internal static void SeedGender(ModelBuilder builder)
        // {
        //     builder.Entity<Gender>().HasData(
        //         new Gender()
        //         {
        //             Name = "Male",
        //             Code = "M",
        //             CreatedAt = DateTime.Now
        //         },
        //         new Gender()
        //         {
        //             Name = "Female",
        //             Code = "F",
        //             CreatedAt= DateTime.Now
        //         }
        //   );
        // }
        // internal static void SeedSuffix(ModelBuilder builder)
        // {
        //     builder.Entity<Suffix>().HasData(             
                
        //         new Suffix()
        //         {
        //             Name = "Mr.",
        //             CreatedAt= DateTime.Now,
        //         },
        //         new Suffix()
        //         {
        //             Name="Mrs.",
        //             CreatedAt= DateTime.Now,
                   
        //         }
                
        //   );
        }
    }
}
