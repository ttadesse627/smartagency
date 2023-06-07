

using AppDiv.SmartAgency.Domain.Entities.Applicants;
using AppDiv.SmartAgency.Domain.Entities.Base;
using AppDiv.SmartAgency.Domain.Enums;

namespace AppDiv.SmartAgency.Domain.Entities;
public class ApplicantProcess : BaseAuditableEntity
{
    public Guid? ProcessId { get; set; }
    public Guid? ApplicantId { get; set; }
    public DateTime? Date { get; set; }
    public ProcessStatus? Status { get; set; } = ProcessStatus.In;
    public ProcessDefinition? Process { get; set; }
    public Applicant? Applicant { get; set; }
}