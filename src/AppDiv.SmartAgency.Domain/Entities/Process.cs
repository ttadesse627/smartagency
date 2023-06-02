

using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities;
public class Process : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public int Step { get; set; }
    public bool IsVisaRequired { get; set; }
    public bool EnjazRequired { get; set; }
    public bool TicketRequired { get; set; }
    public Guid? CountryId { get; set; }
    public LookUp? Country { get; set; }
    public ICollection<ProcessDefinition>? ProcessDefinitions { get; set; }
}