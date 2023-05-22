using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations
{
    public class LetterInformationEntityConfiguration: IEntityTypeConfiguration<LetterInformation>
{
    public void Configure(EntityTypeBuilder<LetterInformation> builder)
    {
        builder.HasOne(lo => lo.Partner)
            .WithOne(p => p.LetterInformation)
            .HasForeignKey<LetterInformation>(fk=> fk.PartnerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
}