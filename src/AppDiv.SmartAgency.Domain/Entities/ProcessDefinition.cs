
using AppDiv.SmartAgency.Domain.Entities.Base;

namespace AppDiv.SmartAgency.Domain.Entities;
public class ProcessDefinition : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public int Step { get; set; }
    public bool RequestApproval { get; set; }
    public int ExpiryInterval { get; set; }
    public Guid ProcessId { get; set; }
    public Process? Process { get; set; }
    public ICollection<ApplicantProcess> ApplicantProcesses { get; set; } = [];
}