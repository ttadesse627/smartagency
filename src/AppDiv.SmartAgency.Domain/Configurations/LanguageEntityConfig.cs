using AppDiv.SmartAgency.Domain.Entities.Applicants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class LanguageEntityConfig : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasOne(lan => lan.LanguageLookUp)
                .WithOne(lk => lk.LookupLanguage)
                .HasForeignKey<Language>(fk => fk.LanguageLookUpId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}