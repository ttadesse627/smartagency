

using AppDiv.SmartAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppDiv.SmartAgency.Domain.Configurations;
public class ProcessDefinitionEntityConfig : IEntityTypeConfiguration<ProcessDefinition>
{
    public void Configure(EntityTypeBuilder<ProcessDefinition> builder)
    {
        builder.HasOne(pd => pd.Process)
            .WithMany(pr => pr.ProcessDefinitions)
            .HasForeignKey(fk => fk.ProcessId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}