using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class OnlineApplicantEntityConfiguration: IEntityTypeConfiguration<OnlineApplicant>
    {
        public void Configure(EntityTypeBuilder<OnlineApplicant> builder)
        {
            builder.HasOne(o => o.MaritialStatus)
                .WithMany(l => l.MaritalStatus)
                .HasForeignKey(o => o.MartialStatusId);
            builder.HasOne(o => o.Experience)
                .WithMany(l => l.Experience)
                .HasForeignKey(o => o.ExperienceId);    
        }

    }
}