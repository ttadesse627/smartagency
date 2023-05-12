using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class ApplicantFollowupStatusConfiguration: IEntityTypeConfiguration<ApplicantFollowupStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicantFollowupStatus> builder)
        {
            builder.HasOne(fs => fs.FollowupStatus)
                .WithMany(l => l.FollowupStatus)
                .HasForeignKey(fs => fs.FollowupStatusId);
            builder.HasOne(fs=> fs.Applicant)
                .WithMany(a => a.ApplicantFollowupStatuses)
                .HasForeignKey(fs=> fs.ApplicantId);  
                 
        }

    }
}